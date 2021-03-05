using UnityEngine;

namespace RectangleTrainer.Mesh2Script
{
    public static class Mesh2Script
    {
        public static string ConvertToScript(Mesh mesh, string classname)
        {
            //start class
            string csContent = "using UnityEngine;\n" +
                               "namespace RectangleTrainer.Mesh2Script.ScriptMesh\n" +
                               "{\n\tpublic static class " + classname + "\n";

            //upto vertices
            csContent += "\t{\n" +
                         "\t\tprivate static Mesh mesh;\n" +
                         "\t\tpublic static Mesh Mesh\n" +
                         "\t\t{\n" +
                         "\t\t\tget\n" +
                         "\t\t\t{\n" +
                         "\t\t\t\tif (mesh == null)\n" +
                         "\t\t\t\t\tmesh = Make();\n\n" +
                         "\t\t\t\treturn mesh;\n" +
                         "\t\t\t}\n" +
                         "\t\t}\n\n" +
                         "\t\tprivate static Mesh Make()\n" +
                         "\t\t{\n";

            //vertices
            csContent += "\t\t\tVector3[] vertices =\n" +
                         "\t\t\t{\n";

            for(int i = 0; i < mesh.vertices.Length; i++)
            {
                csContent += $"\t\t\t\tnew Vector3({mesh.vertices[i].x}f, {mesh.vertices[i].y}f, {mesh.vertices[i].z}f)";
                if (i < mesh.vertices.Length - 1) csContent += ",";
                csContent += "\n";
            }

            csContent += "\t\t\t};\n";


            //triangles
            csContent += "\t\t\tint[] triangles =\n" +
                         "\t\t\t{\n";

            for (int i = 0; i < mesh.triangles.Length; i+=3)
            {
                csContent += $"\t\t\t\t{mesh.triangles[i]}, {mesh.triangles[i+1]}, {mesh.triangles[i+2]}";
                if (i < mesh.triangles.Length - 1) csContent += ",";
                csContent += "\n";
            }

            csContent += "\t\t\t};\n\n";

            //normals
            csContent += "\t\t\tVector3[] normals =\n" +
                         "\t\t\t{\n";

            for (int i = 0; i < mesh.normals.Length; i++)
            {
                csContent += $"\t\t\t\tnew Vector3({mesh.normals[i].x}f, {mesh.normals[i].y}f, {mesh.normals[i].z}f)";
                if (i < mesh.normals.Length - 1) csContent += ",";
                csContent += "\n";
            }

            csContent += "\t\t\t};\n";

            //wrap class up
            csContent += "\t\t\tMesh mesh = new Mesh();\n" +
                         "\t\t\tmesh.vertices = vertices;\n" +
                         "\t\t\tmesh.triangles = triangles;\n" +
                         "\t\t\tmesh.normals = normals;\n\n" +
                         "\t\t\treturn mesh;\n" +
                         "\t\t}\n" +
                         "\t}\n" +
                         "}";

            return csContent;
        }
    }
}
