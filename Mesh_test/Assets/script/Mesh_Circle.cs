using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_Circle : MonoBehaviour {

    private Mesh mesh;
    [SerializeField]
    private Material _mat;

    public PhysicMaterial physicMaterial;

    public float r_in = 1.0f;
    public float r_out = 5.0f;
    public int div_num = 30; //3の倍数にする

    // Use this for initialization
    void Start () {
        int i = 0;
        mesh = new Mesh();
        Vector3[] newVertices = new Vector3[div_num+1];
        Vector2[] newUV = new Vector2[div_num+1];
        int[] newTriangles = new int[div_num*3];

        // 頂点座標の指定.
        //newVertices[0] = new Vector3(0.0f, 1.0f, 0.0f);
        //newVertices[1] = new Vector3(-1.0f, -1.0f, 0.0f);
        //newVertices[2] = new Vector3(1.0f, -1.0f, 0.0f);
        //newVertices[3] = new Vector3(2.0f, 0.0f, 2.0f);

        newVertices[0].x = 0.0f;
        newVertices[0].y = 0.0f;
        newVertices[0].z = 0.0f;
        newUV[0] = new Vector2(0.0f, 0.0f);

        for (i = 0; i < div_num; i++)
        {
            // 頂点座標の指定.
            newVertices[i+1].x = r_out * Mathf.Cos(2.0f * Mathf.PI * (float)(i+1) / (float)div_num);
            newVertices[i+1].y = r_out * Mathf.Sin(2.0f * Mathf.PI * (float)(i+1) / (float)div_num);
            newVertices[i+1].z = 0.0f;

            // UVの指定 (頂点数と同じ数を指定すること).
            if(i%3 == 2)
            {
                newUV[i+1] = new Vector2(0.0f, 0.0f);
            }
            else if (i % 3 == 1)
            {
                newUV[i+1] = new Vector2(0.0f, 1.0f);
            }
            else if (i % 3 == 0)
            {
                newUV[i+1] = new Vector2(1.0f, 1.0f);
            }

        }


        // UVの指定 (頂点数と同じ数を指定すること).
        //newUV[0] = new Vector2(0.0f, 0.0f);
        //newUV[1] = new Vector2(0.0f, 1.0f);
        //newUV[2] = new Vector2(1.0f, 1.0f);
        //newUV[3] = new Vector2(1.0f, 0.0f);

        // 三角形ごとの頂点インデックスを指定.
        //newTriangles[0] = 2;
        //newTriangles[1] = 1;
        //newTriangles[2] = 0;
        //newTriangles[3] = 0;
        //newTriangles[4] = 1;
        //newTriangles[5] = 2;

        for (i = 0; i < newVertices.GetLength(0)-2; i++)
        {
            // 三角形ごとの頂点インデックスを指定.
            newTriangles[3*i] = 0;
            newTriangles[3*i+1] = i+1;
            newTriangles[3*i+2] = i+2;

            //newTriangles[i] = i;
            /*
            if (i % 3 == 0)
            {
                newTriangles[i] =  2 * i;
            }
            else if (i % 3 == 1)
            {
                newTriangles[i] = 2 * i + 1;
            }
            else if (i % 3 == 2)
            {
                newTriangles[i] = 2 *( i + 1);
            }
            */
        }

        //Debug.Log("i=" + i + ": max=" + newTriangles.GetLength(0));

        //最後の三角形の頂点インデックスを指定.
        newTriangles[3 * i] = 0;
        newTriangles[3 * i + 1] = i + 1;
        newTriangles[3 * i + 2] = 1;

        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshFilter>().sharedMesh.name = "myMesh";

        var renderer = GetComponent<MeshRenderer>();
        renderer.material = _mat;

        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        if (!meshCollider) meshCollider = gameObject.AddComponent<MeshCollider>();

        meshCollider.sharedMesh = mesh;
        meshCollider.sharedMaterial = physicMaterial;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
