using UnityEngine;
namespace RectangleTrainer.Mesh2Script.ScriptMesh
{
	public static class Cube_ScriptMesh
	{
		private static Mesh mesh;

        private static float[] vertList = 
		{
            0.5f, -0.5f, 0.5f,-0.5f, -0.5f, 0.5f,0.5f, 0.5f, 0.5f,-0.5f, 0.5f, 0.5f,
			0.5f, 0.5f, -0.5f,-0.5f, 0.5f, -0.5f,0.5f, -0.5f, -0.5f,
			-0.5f, -0.5f, -0.5f,0.5f, 0.5f, 0.5f,-0.5f, 0.5f, 0.5f,
			0.5f, 0.5f, -0.5f,-0.5f, 0.5f, -0.5f,0.5f, -0.5f, -0.5f,
			0.5f, -0.5f, 0.5f,-0.5f, -0.5f, 0.5f,-0.5f, -0.5f, -0.5f,
			-0.5f, -0.5f, 0.5f,-0.5f, 0.5f, 0.5f,-0.5f, 0.5f, -0.5f,
			-0.5f, -0.5f, -0.5f,0.5f, -0.5f, -0.5f,0.5f, 0.5f, -0.5f,
			0.5f, 0.5f, 0.5f,0.5f, -0.5f, 0.5f
		};

		private static int[] triangles =
		{
            0, 2, 3,0, 3, 1,8, 4, 5,8, 5, 9,10, 6, 7,10, 7, 11,12, 13, 14,
			12, 14, 15,16, 17, 18,16, 18, 19,20, 21, 22,20, 22, 23,
		};

        private static float[] nList = 
		{
            0f, 0f, 1f,0f, 0f, 1f,0f, 0f, 1f,0f, 0f, 1f,
			0f, 1f, 0f,0f, 1f, 0f,0f, 0f, -1f,
			0f, 0f, -1f,0f, 1f, 0f,0f, 1f, 0f,
			0f, 0f, -1f,0f, 0f, -1f,0f, -1f, 0f,
			0f, -1f, 0f,0f, -1f, 0f,0f, -1f, 0f,
			-1f, 0f, 0f,-1f, 0f, 0f,-1f, 0f, 0f,
			-1f, 0f, 0f,1f, 0f, 0f,1f, 0f, 0f,
			1f, 0f, 0f,1f, 0f, 0f
		};

        private static float[] uvList = 
		{
            0f, 0f,1f, 0f,0f, 1f,1f, 1f,0f, 1f,1f, 1f,
			0f, 1f,1f, 1f,0f, 0f,1f, 0f,0f, 0f,
			1f, 0f,0f, 0f,0f, 1f,1f, 1f,1f, 0f,
			0f, 0f,0f, 1f,1f, 1f,1f, 0f,0f, 0f,
			0f, 1f,1f, 1f,1f, 0f
		};

		public static Mesh Mesh
		{
			get
			{
				if (mesh == null)
					mesh = Make();

				return mesh;
			}
		}

        private static Vector3[] FloatListToVector3(ref float[] list) {
			Vector3[] vectorArray = new Vector3[list.Length / 3];
            for(int i = 0; i < vectorArray.Length; i ++) {
                vectorArray[i] = new Vector3(list[i * 3], list[i * 3 + 1], list[i * 3 + 2]);
            }

            return vectorArray;
        }

        private static Vector2[] FloatListToVector2(ref float[] list) {
			Vector2[] vectorArray = new Vector2[list.Length / 2];
            for(int i = 0; i < vectorArray.Length; i ++) {
                vectorArray[i] = new Vector2(list[i * 2], list[i * 2 + 1]);
            }

            return vectorArray;
        }

		private static Mesh Make()
		{
			Vector3[] vertices = FloatListToVector3(ref vertList);
			Vector3[] normals = FloatListToVector3(ref nList);
            Vector2[] uv = FloatListToVector2(ref uvList);

			Mesh mesh = new Mesh();
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.normals = normals;
            mesh.uv = uv;

            if(mesh.normals.Length == 0) {
                mesh.RecalculateNormals();
            }

            mesh.RecalculateBounds();

			return mesh;
		}
	}
}
