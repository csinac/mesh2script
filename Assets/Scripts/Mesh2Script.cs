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
            public string message;
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

        public void ConvertToScript(Vector3[] vertices, Vector3[] normals, int[] triangles)
        {
            this.vertices = vertices;
            this.normals = normals;
            this.triangles = triangles;

            status.inProgress = true;

            Thread buildThread = new Thread(Run);
            buildThread.Start();

        }

        public void ConvertToScript(Mesh mesh) => ConvertToScript(mesh.vertices, mesh.normals, mesh.triangles);

        public event Action OnComplete;

        public void Run()
        {
            UpdateStatus("Writing vertices.");
            string vertStr = ArrayToString(vertices);
            UpdateStatus("Writing triangles.");
            string trigStr = ArrayToString(triangles);
            UpdateStatus("Writing normals.");
            string normStr = ArrayToString(normals);

            string output = string.Format(template, classname, vertStr, trigStr, normStr);

            if(File.Exists(path)) {
                File.Delete(path);
            }

            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(output);
            writer.Close();

            status.message = "Done!";
            status.inProgress = false;
            OnComplete?.Invoke();
        }

        private void UpdateStatus(string message, float progress = 0)
        {
            status.message = message;
            status.progress = progress;
            Debug.Log($"{classname}: {message}");
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
