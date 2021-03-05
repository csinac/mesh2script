using UnityEngine;

namespace RectangleTrainer.Mesh2Script
{
    public static class Mesh2Script
    {
        public static string ConvertToScript(Mesh mesh, string classname, string template)
        {
            string vertices = ArrayToString(mesh.vertices);
            string triangles = ArrayToString(mesh.triangles);
            string normals = ArrayToString(mesh.normals);

            return string.Format(template, classname, vertices, triangles, normals);
        }

        private static string ArrayToString(Vector3[] array) {
            string output = "";

            for (int i = 0; i < array.Length; i++)
            {
                output += $"{array[i].x}f, {array[i].y}f, {array[i].z}f";
                if (i < array.Length - 1) output += ",";

                if (i > 0 && i % 3 == 0)
                    output += "\n\t\t\t";
            }

            return output;
        }

        private static string ArrayToString(int[] array)
        {
            string output = "";

            for (int i = 0; i < array.Length; i += 3)
            {
                output += $"{array[i]}, {array[i + 1]}, {array[i + 2]}";
                if (i < array.Length - 1) output += ",";

                if (i > 0 && i % 20 == 0)
                    output += "\n\t\t\t";
            }

            return output;
        }
    }
}
