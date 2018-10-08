using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMesh : MonoBehaviour {
    public Material material;
    public PhysicMaterial physicMaterial;

    public float timeOut;
    private float timeElapsed;
    // Use this for initialization
    void Start () {
        CreateMesh(1);
    }
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;
        CreateMesh((int)timeElapsed+1);

    }

    void CreateMesh(int length)
    {
        //Vector3[] vertices = {
        //    //new Vector3(-1f, -1f, -1f),
        //    //new Vector3(-1f,  1f, -1f),
        //    //new Vector3( 1f,  1f, -1f),
        //    //new Vector3( 1f, -1f, -1f),
        //    //new Vector3(-1f, -1f, 1f),
        //    //new Vector3(-1f,  1f, 1f),
        //    //new Vector3( 1f,  1f, 1f),
        //    //new Vector3( 1f, -1f, 1f)
        //    new Vector3(-2f, -2f, -2f),
        //    new Vector3(-2f,  2f, -2f),
        //    new Vector3( 2f,  2f, -2f),
        //    new Vector3( 2f, -2f, -2f),
        //    new Vector3(-2f, -2f, 2f),
        //    new Vector3(-2f,  2f, 2f),
        //    new Vector3( 2f,  2f, 2f),
        //    new Vector3( 2f, -2f, 2f)
        //    };

        Vector3[] vertices = new Vector3[(length+1) * 4];

        int[] triangles = new int[(length - 1) * 24 + 30];//{ 4, 0, 1, 4, 1, 5 ,3,6,2,3,7,6,7,0,4,7,3,0,1,6,5,1,2,6,6,7,4,6,4,5};
        
        for(int n = 0; n < length + 1; n++)
        {
            vertices[4 * n + 0] = new Vector3(-2f, -2f, -2f + 2*n);
            vertices[4 * n + 1] = new Vector3(-2f, 2f, -2f + 2 * n);
            vertices[4 * n + 2] = new Vector3(2f, 2f, -2f + 2 * n);
            vertices[4 * n + 3] = new Vector3(2f, -2f, -2f + 2 * n);
        }

        int temp = 0;
        for (int n = 0; n < (length - 1) * 24 + 30; n++)
        {
            triangles[n] = 4 * (temp + 1);
            //Debug.Log(triangles[n]);
            triangles[++n] = 4 * temp + 1;
            //Debug.Log(triangles[n]);
            triangles[++n] = 4 * (temp + 1) + 1;
            triangles[++n] = 4 * (temp + 1);
            triangles[++n] = 4 * temp;
            triangles[++n] = 4 * temp + 1;

            triangles[++n] = 4 * temp + 3;
            triangles[++n] = 4 * (temp + 1)+2;
            triangles[++n] = 4 * temp + 2;
            triangles[++n] = 4 * temp + 3;
            triangles[++n] = 4 * (temp + 1)+3;
            triangles[++n] = 4 * (temp + 1)+2;

            triangles[++n] = 4 * (temp + 1)+3;
            triangles[++n] = 4 * temp;
            triangles[++n] = 4 * (temp + 1);
            triangles[++n] = 4 * (temp + 1)+3;
            triangles[++n] = 4 * temp + 3;
            triangles[++n] = 4 * temp;

            triangles[++n] = 4 * temp + 1;
            triangles[++n] = 4 * (temp + 1)+2;
            triangles[++n] = 4 * (temp + 1)+1;
            triangles[++n] = 4 * temp + 1;
            triangles[++n] = 4 * temp + 2;
            triangles[++n] = 4 * (temp + 1)+2;


            if (n == (length - 1) * 24 + 23)
            {
                triangles[++n] = 4 * (temp + 1) + 2;
                triangles[++n] = 4 * (temp + 1) + 3;
                triangles[++n] = 4 * (temp + 1);
                triangles[++n] = 4 * (temp + 1) + 2;
                triangles[++n] = 4 * (temp + 1);
                triangles[++n] = 4 * (temp + 1) + 1;

            }
            temp++;
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
        meshRenderer.sharedMaterial = material;
        meshCollider.sharedMesh = mesh;
        meshCollider.sharedMaterial = physicMaterial;
    }
}
