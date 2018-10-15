using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
	public class FloatArraySubscriber : Subscriber<Messages.Standard.Float32array>
	{
		//private Mesh mesh;
		[SerializeField]
		public Material material;
		public PhysicMaterial physicMaterial;

        //color test
        public Gradient meshColorGradient;
        public float minHeight;
        public float maxHeight;
        //color test

        //public float[] messageData;
        private bool isMessageReceived = false;
		private int size;
        private float[] floatArray;

        protected override void Start()
		{
			base.Start();
			
		}
		private void Update()
		{
			if (isMessageReceived) {
                //Destroy(mesh);
                CreateMesh();
                isMessageReceived = false;
			
			}

		}

		protected override void ReceiveMessage(Messages.Standard.Float32array message)
		{
			int i = 0;
            
			/*foreach (float temp in message.data) {
				messageData[i] = temp;
				i++;
			}
			i = 0;
			*/
			//size = i;
            size = message.data.GetLength(0);
            //Debug.Log(size);
            floatArray = new float[size];

            foreach (float temp in message.data)
            {
                floatArray[i] = temp;
                i++;
            }
			//Debug.Log(floatArray[0]);
            isMessageReceived = true;
		}

		void CreateMesh()
		{

			//Debug.Log (size);

			Vector3[] vertices = new Vector3[size/3];

			int[] triangles = new int[size/3];

            Vector2[] uvs = new Vector2[size/3];

            for (int n = 0; n < size/3; n++)
		    {
                // 頂点座標の指定
                //vertices[n] = new Vector3(messageData[3*n],messageData[3*n+1],messageData[3*n+2]);
                vertices[n] = new Vector3(floatArray[3 * n], floatArray[3 * n + 1], floatArray[3 * n + 2]);

                // 三角形ごとの頂点インデックスを指定(片面)
                triangles[n] = n;

                // UVの指定 (頂点数と同じ数を指定すること).
                //いったんなし
                //uvs[n] = new Vector2(3*n / (float)size, 3*n / (float)size);
            }


            Mesh mesh = new Mesh();
			mesh.vertices = vertices;
			mesh.triangles = triangles;

			mesh.RecalculateNormals();

			MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
			if (!meshFilter) meshFilter = gameObject.AddComponent<MeshFilter>();

			MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
			if (!meshRenderer) meshRenderer = gameObject.AddComponent<MeshRenderer>();

			MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
			if (!meshCollider) meshCollider = gameObject.AddComponent<MeshCollider>();

			meshFilter.mesh = mesh;

            /*色付けテスト*/
            //meshRenderer.sharedMaterial.mainTexture = CreateTexture(vertices);

            meshRenderer.sharedMaterial = material;
			meshCollider.sharedMesh = mesh;
			meshCollider.sharedMaterial = physicMaterial;
		}

        Texture2D CreateTexture(Vector3[] vertices)
        {
            Color[] colorMap = new Color[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                float percent = Mathf.InverseLerp(minHeight, maxHeight, vertices[i].y);
                colorMap[i] = meshColorGradient.Evaluate(percent);
            }
            Texture2D texture = new Texture2D(size, size);

            texture.SetPixels(colorMap);
            texture.Apply();

            return texture;
        }
    }
}
