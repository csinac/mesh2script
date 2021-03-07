using UnityEngine;

namespace RectangleTrainer.Mesh2Script
{
    public abstract class AbstractMeshMaker: ScriptableObject
    {
        abstract public Mesh Generate();
    }
}