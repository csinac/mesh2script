using UnityEngine;

namespace RectangleTrainer.Mesh2Script.Demo
{
    public class LoadScriptMesh : MonoBehaviour
    {
        void Start()
        {
            MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
            MeshFilter mf = gameObject.AddComponent<MeshFilter>();
            renderer.material = new Material(Shader.Find("RT/Mesh2Script/CheckeredTestShader"));

            mf.mesh = ScriptMesh.Sphere_ScriptMesh.Mesh;
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 5);
        }
    }
}
