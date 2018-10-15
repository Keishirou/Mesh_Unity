using UnityEngine;
using System;
using System.Collections;

public class PerlinNoiseMesh : MonoBehaviour
{
    public bool test;
    public Vector2 RadomRange;
    public float timeOut;
    private float timeElapsed;

    public Gradient meshColorGradient;
    public float minHeight;
    public float maxHeight;
    [Range(1, 255)]
    public int size;
    public float vertexDistance = 1f;
    public Material material;
    public PhysicMaterial physicMaterial;

    public PerlinNoiseProperty[] perlinNoiseProperty = new PerlinNoiseProperty[1];
    [System.Serializable]
    public class PerlinNoiseProperty
    {
        public float heightMultiplier = 1f;
        public float scale = 1f;
        public Vector2 offset;
    }

    void Start()
    {
        PerlinNoiseProperty[] p = perlinNoiseProperty;

        if (!test)
        {
            CreateMesh(p);
        }
        else
        {
            CreateMeshTest(p);
        }
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        //if ((update)&&(timeElapsed >= timeOut))
        //{
        //    PerlinNoiseProperty[] per = perlinNoiseProperty;
        //    foreach (PerlinNoiseProperty p in per)
        //    {
        //        p.offset.x = Random.Range(RadomRange.x, RadomRange.y); //test* 10;
        //        p.offset.y = Random.Range(RadomRange.x, RadomRange.y); //test * 10;
        //    }
        //    CreateMesh(per);
        //    timeElapsed = 0.0f;
        //}

        PerlinNoiseProperty[] per = perlinNoiseProperty;
        foreach (PerlinNoiseProperty p in per)
        {
            p.offset.x = timeElapsed*10;// Random.Range(RadomRange.x, RadomRange.y); //test* 10;
            p.offset.y = timeElapsed*10;// Random.Range(RadomRange.x, RadomRange.y); //test * 10;
        }
        CreateMesh(per);

        if(timeElapsed >= timeOut)
            timeElapsed = 0.0f;

    }

    void CreateMesh(PerlinNoiseProperty[] per)
    {
        Vector3[] vertices = new Vector3[size * size];
        System.Random random = new System.Random();

        /*for (int i = 0; i < size * size; i++)
        {
            vertices[i].x = random.Next(0, 255);
            vertices[i].y = random.Next(0, 255);
            vertices[i].z = random.Next(0, 255);
        }*/
        
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {

                float sampleX;
                float sampleZ;
                float y = 0;
                foreach (PerlinNoiseProperty p in per)
                {
                    p.scale = Mathf.Max(0.0001f, p.scale);
                    sampleX = (x + p.offset.x) / p.scale;
                    sampleZ = (z + p.offset.y) / p.scale;
                    y += Mathf.PerlinNoise(sampleX, sampleZ) * p.heightMultiplier;
                }

                vertices[z * size + x] = new Vector3(x * vertexDistance, y, -z * vertexDistance);
            }
        }

        int triangleIndex = 0;
        int[] triangles = new int[(size - 1) * (size - 1) * 6];
        //int[] triangles = new int[size * size * 6];
        /*for (int n = 0; n < size / 3; n++)
        {
            // 三角形ごとの頂点インデックスを指定(片面)
            triangles[n] = n;

        }*/

        for (int z = 0; z < size ; z++)
        {
            for (int x = 0; x < size ; x++)
            {

                int a = z * size + x;
                int b = a + 1;
                int c = a + size;
                int d = c + 1;

                triangles[triangleIndex] = a;
                triangles[triangleIndex + 1] = b;
                triangles[triangleIndex + 2] = c;

                triangles[triangleIndex + 3] = c;
                triangles[triangleIndex + 4] = b;
                triangles[triangleIndex + 5] = d;

                triangleIndex += 6;
            }
        }

        
        Vector2[] uvs = new Vector2[size * size];
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                uvs[z * size + x] = new Vector2(x / (float)size, z / (float)size);
            }
        }



        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();

        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (!meshFilter) meshFilter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (!meshRenderer) meshRenderer = gameObject.AddComponent<MeshRenderer>();
        
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        if (!meshCollider) meshCollider = gameObject.AddComponent<MeshCollider>();

        meshFilter.mesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = CreateTexture(vertices);
        meshRenderer.sharedMaterial = material;
        meshCollider.sharedMesh = mesh;
        meshCollider.sharedMaterial = physicMaterial;
    }

    //動的なサイズの変更テスト
    void CreateMeshTest(PerlinNoiseProperty[] per)
    {
        Vector3[] vertices = new Vector3[size];
        System.Random random = new System.Random();

        //テスト用のfloatArray生成
        float[] floatArray = new float[3*size];

        for (int i = 0; i < 3 * size; i++)
        {
            floatArray[i] = random.Next(0, 255);
        }

        for (int n = 0; n < size / 3; n++)
        {
            // 頂点座標の指定
            //vertices[n] = new Vector3(messageData[3*n],messageData[3*n+1],messageData[3*n+2]);
            vertices[n] = new Vector3(floatArray[3 * n], floatArray[3 * n + 1], floatArray[3 * n + 2]);

            // 三角形ごとの頂点インデックスを指定(片面)
            //triangles[n] = n;

        }


        float min_x = 0.0f;
        float max_x = 0.0f;
        float min_y = 0.0f;
        float max_y = 0.0f;
        //float min_z = 0.0f;
        //float max_z = 0.0f;

        //max_xとmax_yを求める
        for (int i = 0; i < 3 * size; i++)
        {
            if (i % 3 == 0)
            {
                //x座標
                if (floatArray[i] > max_x)
                {
                    max_x = floatArray[i];
                }else if(floatArray[i] < min_x)
                {
                    min_x = floatArray[i];
                }

            }else if (i % 3 == 1)
            {
                //y座標
                if (floatArray[i] > max_y)
                {
                    max_y = floatArray[i];
                }
                else if (floatArray[i] < min_y)
                {
                    min_y = floatArray[i];
                }

            }
            else if (i % 3 == 2)
            {
                //z座標


            }

        }



        /*
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {

                float sampleX;
                float sampleZ;
                float y = 0;
                foreach (PerlinNoiseProperty p in per)
                {
                    p.scale = Mathf.Max(0.0001f, p.scale);
                    sampleX = (x + p.offset.x) / p.scale;
                    sampleZ = (z + p.offset.y) / p.scale;
                    y += Mathf.PerlinNoise(sampleX, sampleZ) * p.heightMultiplier;
                }

                vertices[z * size + x] = new Vector3(x * vertexDistance, y, -z * vertexDistance);
            }
        }*/

        //int triangleIndex = 0;
        //int[] triangles = new int[(size - 1) * (size - 1) * 6];
        int[] triangles = new int[3 * size];
        for (int n = 0; n < size / 3; n++)
        {
            // 三角形ごとの頂点インデックスを指定(片面)
            triangles[n] = n;

        }
        /*
        for (int z = 0; z < size - 1; z++)
        {
            for (int x = 0; x < size - 1; x++)
            {

                int a = z * size + x;
                int b = a + 1;
                int c = a + size;
                int d = c + 1;

                triangles[triangleIndex] = a;
                triangles[triangleIndex + 1] = b;
                triangles[triangleIndex + 2] = c;

                triangles[triangleIndex + 3] = c;
                triangles[triangleIndex + 4] = b;
                triangles[triangleIndex + 5] = d;

                triangleIndex += 6;
            }
        }*/

        Vector2[] uvs = new Vector2[size * size];
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                uvs[z * size + x] = new Vector2(x / (float)size, z / (float)size);
            }
        }



        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();

        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (!meshFilter) meshFilter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (!meshRenderer) meshRenderer = gameObject.AddComponent<MeshRenderer>();

        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        if (!meshCollider) meshCollider = gameObject.AddComponent<MeshCollider>();

        meshFilter.mesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = CreateTexture(vertices);
        meshRenderer.sharedMaterial = material;
        meshCollider.sharedMesh = mesh;
        meshCollider.sharedMaterial = physicMaterial;
    }

    void OnValidate()
    {
        PerlinNoiseProperty[] p = perlinNoiseProperty;
        CreateMesh(p);
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

    void RandomArray(Vector3[] vertices)
    {
        int size = vertices.Length;
        System.Random random = new System.Random();

        for(int i=0;i<size; i++)
        {
            vertices[i].x = random.Next(0, 255);
            vertices[i].y = random.Next(0, 255);
            vertices[i].z = random.Next(0, 255);
        }
    }
}