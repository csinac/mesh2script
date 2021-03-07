using UnityEngine;

namespace RectangleTrainer.Mesh2Script.Demo
{
    public class LoadScriptMesh : MonoBehaviour
    {
        void Start()
        {
            MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
            MeshFilter mf = gameObject.AddComponent<MeshFilter>();
            renderer.material = new Material(Shader.Find("Standard"));

            mf.mesh = ScriptMesh.ScriptMesh_Wheel_Slice.Mesh;
        }
    }
}
