using UnityEngine;
namespace RectangleTrainer.Mesh2Script.ScriptMesh
{
	public static class Wheel_Slice_ScriptMesh
	{
		private static Mesh mesh;

        private static float[] vertList = 
		{
            0f, 0f, 0.1f,0.9659258f, -0.258819f, 0.1f,0.9807853f, -0.1950903f, 0.1f,0.9914449f, -0.1305262f, 0.1f,
			0.9978589f, -0.06540313f, 0.1f,1f, 0f, 0.1f,0.9978589f, 0.06540313f, 0.1f,
			0.9914449f, 0.1305262f, 0.1f,0.9807853f, 0.1950903f, 0.1f,0.9659258f, 0.258819f, 0.1f,
			0f, 0f, -0.1f,0.9659258f, -0.258819f, -0.1f,0.9807853f, -0.1950903f, -0.1f,
			0.9914449f, -0.1305262f, -0.1f,0.9978589f, -0.06540313f, -0.1f,1f, 0f, -0.1f,
			0.9978589f, 0.06540313f, -0.1f,0.9914449f, 0.1305262f, -0.1f,0.9807853f, 0.1950903f, -0.1f,
			0.9659258f, 0.258819f, -0.1f,0f, 0f, 0.1f,0.9659258f, -0.258819f, 0.1f,
			0.9807853f, -0.1950903f, 0.1f,0.9914449f, -0.1305262f, 0.1f,0.9978589f, -0.06540313f, 0.1f,
			1f, 0f, 0.1f,0.9978589f, 0.06540313f, 0.1f,0.9914449f, 0.1305262f, 0.1f,
			0.9807853f, 0.1950903f, 0.1f,0.9659258f, 0.258819f, 0.1f,0f, 0f, -0.1f,
			0.9659258f, -0.258819f, -0.1f,0.9807853f, -0.1950903f, -0.1f,0.9914449f, -0.1305262f, -0.1f,
			0.9978589f, -0.06540313f, -0.1f,1f, 0f, -0.1f,0.9978589f, 0.06540313f, -0.1f,
			0.9914449f, 0.1305262f, -0.1f,0.9807853f, 0.1950903f, -0.1f,0.9659258f, 0.258819f, -0.1f,
			0f, 0f, 0.1f,0f, 0f, 0.1f,0f, 0f, -0.1f,
			0f, 0f, -0.1f,0.9659258f, -0.258819f, 0.1f,0.9659258f, -0.258819f, -0.1f,
			0.9659258f, 0.258819f, 0.1f,0.9659258f, 0.258819f, -0.1f
		};

		private static int[] triangles =
		{
            40, 42, 45,40, 45, 44,41, 47, 43,41, 46, 47,0, 1, 2,0, 2, 3,0, 3, 4,
			0, 4, 5,0, 5, 6,0, 6, 7,0, 7, 8,0, 8, 9,10, 12, 11,
			10, 13, 12,10, 14, 13,10, 15, 14,10, 16, 15,10, 17, 16,10, 18, 17,
			10, 19, 18,0, 0, 0,0, 0, 0,21, 31, 32,21, 32, 22,22, 32, 33,
			22, 33, 23,23, 33, 34,23, 34, 24,24, 34, 35,24, 35, 25,25, 35, 36,
			25, 36, 26,26, 36, 37,26, 37, 27,27, 37, 38,27, 38, 28,28, 38, 39,
			28, 39, 29,0, 0, 0,0, 0, 0,
		};

        private static float[] nList = 
		{
            0f, 0f, 1f,0f, 0f, 1f,0f, 0f, 1f,0f, 0f, 1f,
			0f, 0f, 1f,0f, 0f, 1f,0f, 0f, 1f,
			0f, 0f, 1f,0f, 0f, 1f,0f, 0f, 1f,
			0f, 0f, -1f,0f, 0f, -1f,0f, 0f, -1f,
			0f, 0f, -1f,0f, 0f, -1f,0f, 0f, -1f,
			0f, 0f, -1f,0f, 0f, -1f,0f, 0f, -1f,
			0f, 0f, -1f,0f, 0f, 0f,0.973877f, -0.227076f, 0f,
			0.9828556f, -0.1843773f, 0f,0.9928101f, -0.1197004f, 0f,0.9985132f, -0.05451082f, 0f,
			0.9999405f, 0.01091146f, 0f,0.9970859f, 0.07628727f, 0f,0.9899615f, 0.1413371f, 0f,
			0.9785981f, 0.2057807f, 0f,0.973877f, 0.227076f, 0f,0f, 0f, 0f,
			0.973877f, -0.227076f, 0f,0.9785981f, -0.2057807f, 0f,0.9899615f, -0.1413371f, 0f,
			0.9970859f, -0.07628727f, 0f,0.9999405f, -0.01091146f, 0f,0.9985132f, 0.05451082f, 0f,
			0.9928101f, 0.1197004f, 0f,0.9828556f, 0.1843773f, 0f,0.973877f, 0.227076f, 0f,
			-0.258819f, -0.9659258f, 0f,-0.258819f, 0.9659258f, 0f,-0.258819f, -0.9659258f, 0f,
			-0.258819f, 0.9659258f, 0f,-0.258819f, -0.9659258f, 0f,-0.258819f, -0.9659258f, 0f,
			-0.258819f, 0.9659258f, 0f,-0.258819f, 0.9659258f, 0f
		};

        private static float[] uvList = 
		{
            
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
