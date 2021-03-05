using UnityEngine;

public class LoadScriptedMesh : MonoBehaviour
{
    void Start()
    {
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        renderer.material = new Material(Shader.Find("Standard"));

        mf.mesh = RectangleTrainer.Mesh2Script.ScriptMesh.Mesh_Sphere_hardcoded.Mesh;
    }
}
