using UnityEngine;
using UnityEditor;
using System.IO;
using System.Threading;

namespace RectangleTrainer.Mesh2Script.Editor
{
    public class Mesh2ScriptWindow : EditorWindow
    {
        Mesh inputMesh;
        MonoScript inputScript;

        string saveFolder = "RT Script Meshes";
        string scriptPrefix = "ScriptMesh";
        string scriptName;
        string scriptTemplate = "";

        Mesh2Script scriptMaker;
        bool refresh = false;
        MeshSource source;

        #region Window Stuff
        [MenuItem("Rectangle Trainer/Mesh to Script")]
        static void Init()
        {
            Mesh2ScriptWindow window = (Mesh2ScriptWindow)GetWindow(typeof(Mesh2ScriptWindow));
            window.titleContent = new GUIContent("Mesh to Script");
            window.Show();
            window.minSize = new Vector2(200, 230);
        }

        private void LoadTemplate()
        {
            string[] templateGuid = AssetDatabase.FindAssets("ScriptMeshTemplate");
            if (templateGuid.Length > 0)
            {
                string templatePath = AssetDatabase.GUIDToAssetPath(templateGuid[0]);
                TextAsset templateAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(templatePath);
                scriptTemplate = templateAsset.text;
            }
        }

        private void OnEnable()
        {
            LoadTemplate();
        }
        #endregion

        #region On GUI
        void SourceTypeSection()
        {
            EditorGUI.BeginChangeCheck();
            source = (MeshSource)EditorGUILayout.EnumPopup("Mesh Source", source);

            if(EditorGUI.EndChangeCheck())
            {
                inputScript = null;
                inputMesh = null;
            }
        }

        void InputSection()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.LabelField("Input Mesh", EditorStyles.boldLabel);

            if (source == MeshSource.mesh)
            {
                inputMesh = (Mesh)EditorGUILayout.ObjectField(inputMesh, typeof(Mesh), true);
            }
            else if (source == MeshSource.script)
            {
                inputScript = (MonoScript)EditorGUILayout.ObjectField(inputScript, typeof(MonoScript), false);
                if (InputIsMeshMakingScript())
                {
                    GenerateMeshFromScript();
                }
                else
                {
                    inputScript = null;
                }
            }

            if (inputMesh)
            {
                if (EditorGUI.EndChangeCheck())
                {
                    scriptName = FormatName(inputMesh.name);
                }

                EditorGUILayout.LabelField("Script Name", EditorStyles.boldLabel);
                scriptName = EditorGUILayout.TextField(FormatName(scriptName));

                string helpboxText = $"{Filename}\n~{SizeMaxEstimate()}";
                EditorGUILayout.HelpBox(helpboxText, MessageType.Info);
            }
        }

        void SaveSection()
        {
            EditorGUILayout.LabelField("Save Folder", EditorStyles.boldLabel);
            saveFolder = EditorGUILayout.TextField(saveFolder);

            EditorGUILayout.Space();
            EditorGUI.BeginDisabledGroup(inputMesh == null);
            if (GUILayout.Button("Convert To Script"))
            {
                if (!AssetDatabase.IsValidFolder($"Assets/{saveFolder}"))
                    AssetDatabase.CreateFolder("Assets", saveFolder);

                BuildMeshScript();

            }
            EditorGUI.EndDisabledGroup();
        }

        private void ShowProgressBar()
        {
            if (scriptMaker == null)
                return;

            Rect r = EditorGUILayout.BeginVertical();
            EditorGUI.ProgressBar(r, scriptMaker.status.progress, scriptMaker.status.message);
            GUILayout.Space(18);
            EditorGUILayout.EndVertical();
        }

        void OnGUI()
        {

            SourceTypeSection();
            InputSection();
            SaveSection();

            ShowProgressBar();
        }
        #endregion

        #region Accessors
        private bool InProgress
        {
            get => scriptMaker != null && scriptMaker.status.inProgress;
        }

        string Filename
        {
            get => $"{saveFolder}/{Classname}.cs";
        }
        string Classname
        {
            get => $"{scriptPrefix}_{scriptName}";
        }
        #endregion

        #region Helpers
        private bool InputIsMeshMakingScript()
        {
            if (!inputScript)
            {
                return false;
            }

            System.Type scriptType = inputScript.GetClass();
            if (scriptType == null)
            {
                return false;
            }

            return scriptType.IsSubclassOf(typeof(AbstractMeshMaker));
        }

        void GenerateMeshFromScript()
        {
            System.Type scriptType = inputScript.GetClass();
            AbstractMeshMaker meshMaker = CreateInstance(scriptType) as AbstractMeshMaker;
            inputMesh = meshMaker.Generate();
        }

        private void Update()
        {
            if(InProgress)
            {
                Repaint();
            }

            if(refresh)
            {
                AssetDatabase.Refresh();
                refresh = false;
            }
        }

        private string SizeMaxEstimate()
        {
            int vertSize = inputMesh.vertexCount * 3 * 13 * 2; //three axis, max. floating decimals, commas and space, position and normal
            int trigSize = inputMesh.triangles.Length * Mathf.CeilToInt(Mathf.Log10(inputMesh.triangles.Length));

            float sum = vertSize + trigSize + scriptTemplate.Length;
            int kpow = 0;

            while(sum > 1024)
            {
                sum /= 1024;
                kpow++;
            }

            string[] unit = { "b", "kb", "mb" };

            return $"{sum.ToString("N1")} {unit[kpow]}";
        }

        void BuildMeshScript()
        {
            if(scriptMaker == null)
            {
                scriptMaker = new Mesh2Script();
                scriptMaker.OnComplete += () => refresh = true;
            }

            string path = "Assets/" + Filename;

            scriptMaker.Initialize(path, Classname, scriptTemplate);
            scriptMaker.ConvertToScript(inputMesh);

        }
 

        string FormatName(string unformatted)
        {
            string formatted = "";

            for(int i = 0; i < unformatted.Length; i++)
            {
                char c = unformatted[i];
                if ((c >= 'a' && c <= 'z') ||
                    (c >= 'A' && c <= 'Z'))
                {
                    formatted += c;
                }
                else if(c == ' ' || c == '-' || c == '_' || c == '.')
                {
                    formatted += '_';
                }
            }

            return formatted;
        }
        #endregion
    }
}