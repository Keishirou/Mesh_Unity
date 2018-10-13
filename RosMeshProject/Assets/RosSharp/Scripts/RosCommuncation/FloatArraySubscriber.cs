using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
	public class FloatArraySubscriber : Subscriber<Messages.Standard.Float32array>
	{
		private Mesh mesh;
		[SerializeField]
		public Material material;
		public PhysicMaterial physicMaterial;

		public float[] messageData;
		private bool isMessageReceived = false;
		private int size;
		
		protected override void Start()
		{
			base.Start();
			
		}
		private void Update()
		{
			if (isMessageReceived) {

                CreateMesh();
                isMessageReceived = false;
			
			}

		}

		protected override void ReceiveMessage(Messages.Standard.Float32array message)
		{
			int i = 0;
			foreach (float temp in message.data) {
				messageData[i] = temp;
				i++;
			}
			size = i;
			isMessageReceived = true;
		}

		void CreateMesh()
		{

			//Debug.Log (size);

			Vector3[] vertices = new Vector3[size/3];

			int[] triangles = new int[size/3];

		 for(int n = 0; n < size/3; n++)
		{
                // 頂点座標の指定
                vertices[n] = new Vector3(messageData[3*n],messageData[3*n+1],messageData[3*n+2]);

                // 三角形ごとの頂点インデックスを指定(katamen)
                triangles[n] = n;
			
		}

            // UVの指定 (頂点数と同じ数を指定すること).
            //ittannnashide

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
			meshRenderer.sharedMaterial = material;
			meshCollider.sharedMesh = mesh;
			meshCollider.sharedMaterial = physicMaterial;
		}
	}
}
