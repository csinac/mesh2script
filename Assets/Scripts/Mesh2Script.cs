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
        Vector2[] uv;

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

        public void ConvertToScript(Vector3[] vertices, Vector3[] normals, int[] triangles, Vector2[] uv)
        {
            this.vertices = vertices;
            this.normals = normals;
            this.triangles = triangles;
            this.uv = uv;

            status.inProgress = true;

            Thread buildThread = new Thread(Run);
            buildThread.Start();

        }

        public void ConvertToScript(Mesh mesh) => ConvertToScript(mesh.vertices, mesh.normals, mesh.triangles, mesh.uv);

        public event Action OnComplete;

        public void Run()
        {
            UpdateStatus("Writing vertices.");
            string vertStr = ArrayToString(vertices);

            string trigStr = "";
            if (triangles != null)
            {
                UpdateStatus("Writing triangles.");
                trigStr = ArrayToString(triangles);
            }

            string normStr = "";
            if (normals != null)
            {
                UpdateStatus("Writing normals.");
                normStr = ArrayToString(normals);
            }

            string uvStr = "";
            if (uv != null)
            {
                UpdateStatus("Writing UVs.");
                uvStr = ArrayToString(uv);
            }

            string output = string.Format(template, classname, vertStr, trigStr, normStr, uvStr);

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
        }

        private string ArrayToStringLoopIteration(string template, int linebreak, int total, int index, params string[] values)
        {
            string line = string.Format(template, values);
            if (index < total - 1) line += ",";

            if (index > 0 && index % linebreak == 0)
                line += "\n\t\t\t";

            status.progress = 1f * index / total;

            return line;
        }

        private string ArrayToString(Vector3[] array) {

            string output = "";
            string template = "{0}f, {1}f, {2}f";

            for (int i = 0; i < array.Length; i++)
            {
                output += ArrayToStringLoopIteration(template, 3, array.Length, i, array[i].x.ToString(), array[i].y.ToString(), array[i].z.ToString());
            }

            return output;
        }

        private string ArrayToString(Vector2[] array)
        {
            string output = "";
            string template = "{0}f, {1}f";

            for (int i = 0; i < array.Length; i++)
            {
                output += ArrayToStringLoopIteration(template, 5, array.Length, i, array[i].x.ToString(), array[i].y.ToString());
            }

            return output;
        }

        private string ArrayToString(int[] array)
        {
            string output = "";
            string template = "{0}, {1}, {2}";

            for (int i = 0; i < array.Length; i += 3)
            {
                output += ArrayToStringLoopIteration(template, 18, array.Length, i, array[i].ToString(), array[i + 1].ToString(), array[i + 2].ToString());
            }

            return output;
        }
    }
}
