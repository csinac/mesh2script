using UnityEngine;
using UnityEditor;
using System.IO;

namespace RectangleTrainer.Mesh2Script
{
    public class Mesh2ScriptWindow : EditorWindow
    {
        Mesh inputMesh;
        string saveFolder = "RT Hardcoded Meshes";
        string scriptPrefix = "Mesh";
        string scriptSuffix = "hardcoded";
        string scriptName;

        // Add menu named "My Window" to the Window menu
        [MenuItem("Rectangle Trainer/Mesh to Script")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            Mesh2ScriptWindow window = (Mesh2ScriptWindow)GetWindow(typeof(Mesh2ScriptWindow));
            window.titleContent = new GUIContent("Mesh to Script");
            window.Show();
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

                EditorGUILayout.HelpBox(Filename, MessageType.Info);
            }

            EditorGUILayout.LabelField("Save Folder", EditorStyles.boldLabel);
            saveFolder = EditorGUILayout.TextField(saveFolder);

            EditorGUILayout.Space();
            EditorGUI.BeginDisabledGroup(inputMesh == null); 
            if (GUILayout.Button("Convert To Script"))
            {
                if(!AssetDatabase.IsValidFolder($"Assets/{saveFolder}"))
                {
                    AssetDatabase.CreateFolder("Assets", saveFolder);
                }

                WriteString();

            }
            EditorGUI.EndDisabledGroup();
        }

        private string SizeEstimation()
        {
//            inputMesh.vertexCount;
            return "";
        }

        void WriteString()
        {
            string path = "Assets/" + Filename;

            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(Mesh2Script.ConvertToScript(inputMesh, Classname));
            writer.Close();
            AssetDatabase.Refresh();
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