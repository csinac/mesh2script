using UnityEngine;

namespace RectangleTrainer.Mesh2Script.Demo
{
    public class LoadScriptMesh : MonoBehaviour
    {
        private Mesh mesh
        {
            get
            {
                return ScriptMesh.Wheel_Slice_ScriptMesh.Mesh;
            }
        }

        void Start()
        {
            MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
            MeshFilter mf = gameObject.AddComponent<MeshFilter>();
            mf.mesh = mesh;
            renderer.material = new Material(Shader.Find("RT/Mesh2Script/CheckeredTestShader"));
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 5);
        }

        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
            {
                Gizmos.color = Color.green;
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
                Gizmos.DrawWireMesh(mesh);
            }
        }
    }
}
