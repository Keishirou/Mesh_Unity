using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class DynamicCreateMesh : MonoBehaviour {

    [SerializeField]
    private Material _mat;

    private void Start()
    {
        var mesh = new Mesh();
        mesh.vertices = new Vector3[] {
            new Vector3 (0, 1f),
            new Vector3 (1f, -1f),
            new Vector3 (-1f, -1f),
        };
        mesh.triangles = new int[] {
            0, 1, 2
        };
        mesh.RecalculateNormals();
        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;

        var renderer = GetComponent<MeshRenderer>();
        renderer.material = _mat;
    }
}
