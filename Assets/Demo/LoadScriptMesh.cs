using UnityEngine;

public class LoadScriptMesh : MonoBehaviour
{
    void Start()
    {
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        renderer.material = new Material(Shader.Find("Standard"));

        mf.mesh = RectangleTrainer.Mesh2Script.ScriptMesh.Mesh_dragon_hardcoded.Mesh;
    }
}
