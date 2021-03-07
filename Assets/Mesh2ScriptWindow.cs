using UnityEngine;
using UnityEditor;
using System.IO;
using System.Threading;

namespace RectangleTrainer.Mesh2Script
{
    public class Mesh2ScriptWindow : EditorWindow
    {
        Mesh inputMesh;
        string saveFolder = "RT Hardcoded Meshes";
        string scriptPrefix = "Mesh";
        string scriptSuffix = "hardcoded";
        string scriptName;
        string scriptTemplate = "";

        Mesh2Script scriptMaker;
        bool refresh = false;

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
        void OnGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Input Mesh", EditorStyles.boldLabel);
            inputMesh = (Mesh)EditorGUILayout.ObjectField(inputMesh, typeof(Mesh), true);

            if(inputMesh)
            {
                if (EditorGUI.EndChangeCheck())
                    scriptName = FormatName(inputMesh.name);

                EditorGUILayout.LabelField("Script Name", EditorStyles.boldLabel);
                scriptName = EditorGUILayout.TextField(FormatName(scriptName));

                string helpboxText = $"{Filename}\n~{SizeMaxEstimate()}";
                EditorGUILayout.HelpBox(helpboxText, MessageType.Info);
            }

            EditorGUILayout.LabelField("Save Folder", EditorStyles.boldLabel);
            saveFolder = EditorGUILayout.TextField(saveFolder);

            EditorGUILayout.Space();
            EditorGUI.BeginDisabledGroup(inputMesh == null); 
            if (GUILayout.Button("Convert To Script"))
            {
                if(!AssetDatabase.IsValidFolder($"Assets/{saveFolder}"))
                    AssetDatabase.CreateFolder("Assets", saveFolder);

                BuildMeshScript();

            }
            EditorGUI.EndDisabledGroup();

            ShowProgressBar();
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

        private bool InProgress
        {
            get => scriptMaker != null && scriptMaker.status.inProgress;
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

        string Filename
        {
            get => $"{saveFolder}/{Classname}.cs";
        }
        string Classname
        {
            get => $"{scriptPrefix}_{scriptName}_{scriptSuffix}";
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
    }
}