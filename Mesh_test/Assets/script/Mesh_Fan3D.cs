using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshRenderer))]
//[RequireComponent(typeof(MeshFilter))]

public class Mesh_Fan3D : MonoBehaviour
{
    private Mesh mesh;
    [SerializeField]
    private Material _mat;

    public PhysicMaterial physicMaterial;

    public float r_in = 3.0f; //内側の円の半径
    public float r_out = 5.0f; //外側の円の半径
    public int div_num = 30; //3の倍数にする

    public int central_angle = 360; //中心角

    float R_in = 0.0f; //内側の円の半径
    float R_out = 0.0f; //外側の円の半径
    int Div_num = 0; //3の倍数にする
    int Central_angle = 0; //中心角

    // Use this for initialization
    void Start()
    {
        //扇型Meshの描画
        CreateMesh(r_in, r_out, div_num, central_angle);
    }

    // Update is called once per frame
    void Update()
    {

        //前のフレームで描画した値と異なる場合再描画
        if ((R_in != r_in) || (R_out != r_out) || (Div_num != div_num) || (Central_angle != central_angle))
        {
            R_in = r_in; //内側の円の半径
            R_out = r_out; //外側の円の半径
            Div_num = div_num; //3の倍数にする
            Central_angle = central_angle; //中心角
            CreateMesh(R_in, R_out, Div_num, Central_angle);//扇型Meshの描画
        }

    }

    void CreateMesh(float r_in, float r_out, int div_num, int central_angle)
    {
        mesh = new Mesh();
        //Vector3[] newVertices = new Vector3[div_num * 2];
        Vector3[] frontVertices = new Vector3[div_num * 2];
        Vector3[] backVertices = new Vector3[div_num * 2];
        Vector2[] newUV = new Vector2[div_num * 2 * 2];

        int i = 0;
        int temp_angle = 0;

        for (i = 0; i < 2 * div_num; i++)
        {
            // 頂点座標の指定.
            if (i < div_num)
            {
                //newVertices[i].x = r_out * Mathf.Cos(2.0f * Mathf.PI * (float)i / (float)div_num);
                //newVertices[i].y = r_out * Mathf.Sin(2.0f * Mathf.PI * (float)i / (float)div_num);
                //newVertices[i].z = 0.0f;
                //newVertices[i] = newVertices[i] + this.transform.localPosition;
                frontVertices[i].x = r_out * Mathf.Cos(2.0f * Mathf.PI * (float)i / (float)div_num);
                frontVertices[i].y = r_out * Mathf.Sin(2.0f * Mathf.PI * (float)i / (float)div_num);
                frontVertices[i].z = 1.0f;
                frontVertices[i] = frontVertices[i] + this.transform.localPosition;
                backVertices[i].x = r_out * Mathf.Cos(2.0f * Mathf.PI * (float)i / (float)div_num);
                backVertices[i].y = r_out * Mathf.Sin(2.0f * Mathf.PI * (float)i / (float)div_num);
                backVertices[i].z = -1.0f;
                backVertices[i] = backVertices[i] + this.transform.localPosition;
            }
            else
            {
                //newVertices[i].x = r_in * Mathf.Cos(2.0f * Mathf.PI * ((float)i + 0.5f) / (float)div_num);
                //newVertices[i].y = r_in * Mathf.Sin(2.0f * Mathf.PI * ((float)i + 0.5f) / (float)div_num);
                //newVertices[i].z = 0.0f;
                //newVertices[i] = newVertices[i] + this.transform.localPosition;
                frontVertices[i].x = r_in * Mathf.Cos(2.0f * Mathf.PI * ((float)i + 0.5f) / (float)div_num);
                frontVertices[i].y = r_in * Mathf.Sin(2.0f * Mathf.PI * ((float)i + 0.5f) / (float)div_num);
                frontVertices[i].z = 1.0f;
                frontVertices[i] = frontVertices[i] + this.transform.localPosition;
                backVertices[i].x = r_in * Mathf.Cos(2.0f * Mathf.PI * ((float)i + 0.5f) / (float)div_num);
                backVertices[i].y = r_in * Mathf.Sin(2.0f * Mathf.PI * ((float)i + 0.5f) / (float)div_num);
                backVertices[i].z = -1.0f;
                backVertices[i] = backVertices[i] + this.transform.localPosition;
            }

            //円をどこで切るのかを決定
            if (central_angle > (180.0f * (float)i / (float)div_num))
            {
                temp_angle = i;
            }


            //// UVの指定 (頂点数と同じ数を指定すること).
            //if (i % 3 == 2)
            //{
            //    newUV[i] = new Vector2(0.0f, 0.0f);
            //}
            //else if (i % 3 == 1)
            //{
            //    newUV[i] = new Vector2(0.0f, 1.0f);
            //}
            //else if (i % 3 == 0)
            //{
            //    newUV[i] = new Vector2(1.0f, 1.0f);
            //}

        }

        Vector3[] newVertices = new Vector3[div_num * 2 * 2];
        frontVertices.CopyTo(newVertices, 0);
        backVertices.CopyTo(newVertices, frontVertices.Length);

        for (i = 0; i < 4 * div_num; i++)
        {
            // UVの指定 (頂点数と同じ数を指定すること).
            if (i % 3 == 2)
            {
                newUV[i] = new Vector2(0.0f, 0.0f);
            }
            else if (i % 3 == 1)
            {
                newUV[i] = new Vector2(0.0f, 1.0f);
            }
            else if (i % 3 == 0)
            {
                newUV[i] = new Vector2(1.0f, 1.0f);
            }
        }
        //　決めた範囲のみ描画する

        //int[] newTriangles = new int[temp_angle*3+3];
        int[] frontTriangles = new int[temp_angle * 3 + 3];
        int[] backTriangles = new int[temp_angle * 3 + 3];

        /*メッシュ生成*/
        for (i = 0; i < temp_angle - 1; i++)
        {
            // 三角形ごとの頂点インデックスを指定.
            if (i % 2 == 0)
            {
                /*表*/
                frontTriangles[3 * i] = i / 2;
                frontTriangles[3 * i + 1] = i / 2 + 1;
                frontTriangles[3 * i + 2] = i / 2 + div_num;

                /*裏*/
                backTriangles[3 * i] = i / 2 + 3 * div_num;
                backTriangles[3 * i + 1] = i / 2 + 1 + 2 * div_num;
                backTriangles[3 * i + 2] = i / 2 + 2 * div_num;
            }
            else
            {
                /*表*/
                frontTriangles[3 * i] = i / 2 + div_num + 1;
                frontTriangles[3 * i + 1] = i / 2 + div_num;
                frontTriangles[3 * i + 2] = i / 2 + 1;

                /*裏*/
                backTriangles[3 * i] = i / 2 + 1 + 2 * div_num;
                backTriangles[3 * i + 1] = i / 2 + 3 * div_num;
                backTriangles[3 * i + 2] = i / 2 + 3 * div_num + 1;
            }
        }

        //端の部分を補う
        /*表*/
        frontTriangles[3 * i] = 0;
        frontTriangles[3 * i + 1] = div_num;
        frontTriangles[3 * i + 2] = 2 * div_num - 1;

        /*裏*/
        backTriangles[3 * i] = 4 * div_num - 1;
        backTriangles[3 * i + 1] = 3 * div_num;
        backTriangles[3 * i + 2] = 2 * div_num;

        i++;

        /*表*/
        frontTriangles[3 * i] = 0;
        frontTriangles[3 * i + 1] = div_num * 2 - 1;
        frontTriangles[3 * i + 2] = div_num - 1;

        /*裏*/
        backTriangles[3 * i] = 3 * div_num - 1;
        backTriangles[3 * i + 1] = 4 * div_num - 1;
        backTriangles[3 * i + 2] = 2 * div_num;


        int[] newTriangles = new int[frontTriangles.Length + backTriangles.Length];

        frontTriangles.CopyTo(newTriangles, 0);
        backTriangles.CopyTo(newTriangles, frontTriangles.Length);

        //Mesh生成開始
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
        meshCollider.sharedMaterial = physicMaterial; //衝突判定付与
    }

}
