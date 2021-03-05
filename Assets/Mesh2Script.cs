using UnityEngine;
using System.Threading;
using System.IO;
using System;


namespace RectangleTrainer.Mesh2Script
{
    public class Mesh2Script
    {
        public struct Status
        {
            private string _message;
            public string message
            {
                get
                {
                    return _message;
                }
                set
                {
                    _message = value;
                    progress = 0;
                }
            }
            public float progress;
            public bool inProgress;
        }

        Vector3[] vertices;
        Vector3[] normals;
        int[] triangles;

        string classname;
        string template;
        string path;

        public Status status = new Status();

        public void Initialize(string path, string classname, string template)
        {
            status.message = "";

            this.path = path;
            this.classname = classname;
            this.template = template;
        }

        public void ConvertToScript(Mesh mesh)
        {
            vertices = mesh.vertices;
            normals = mesh.normals;
            triangles = mesh.triangles;

            status.inProgress = true;

            Thread buildThread = new Thread(Run);
            buildThread.Start();

        }

        public event Action OnComplete;

        public void Run()
        {
            status.message = "Writing vertices.";
            string vertStr = ArrayToString(vertices);
            status.message = "Writing triangles.";
            string trigStr = ArrayToString(triangles);
            status.message = "Writing normals.";
            string normStr = ArrayToString(normals);

            string output = string.Format(template, classname, vertStr, trigStr, normStr);

            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(output);
            writer.Close();

            status.message = "Done!";
            status.inProgress = false;
            OnComplete?.Invoke();
        }

        private string ArrayToString(Vector3[] array) {

            string output = "";

            for (int i = 0; i < array.Length; i++)
            {
                output += $"{array[i].x}f, {array[i].y}f, {array[i].z}f";
                if (i < array.Length - 1) output += ",";

                if (i > 0 && i % 3 == 0)
                    output += "\n\t\t\t";

                status.progress = 1f * i / array.Length;
            }

            return output;
        }

        private string ArrayToString(int[] array)
        {
            string output = "";

            for (int i = 0; i < array.Length; i += 3)
            {
                output += $"{array[i]}, {array[i + 1]}, {array[i + 2]}";
                if (i < array.Length - 1) output += ",";

                if (i > 0 && i % 20 == 0)
                    output += "\n\t\t\t";

                status.progress = 1f * i / array.Length;
            }

            return output;
        }
    }
}
