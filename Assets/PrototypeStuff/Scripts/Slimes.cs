using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimes : MonoBehaviour
{
    Mesh mesh;
    MeshFilter filter;
    MeshCollider meshCollider;

    // Start is called before the first frame update
    void Start()
    {
        filter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        Mesh oldMesh = filter.sharedMesh;

        mesh = new Mesh();

        mesh.vertices = oldMesh.vertices;
        mesh.triangles = oldMesh.triangles;
        mesh.uv = oldMesh.uv;

        filter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    public void ShrinkTriangle(int triIndex)
    {
        //Debug.Log(triIndex);
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        vertices[triangles[triIndex * 3 + 0]] *= 0.9f;
        vertices[triangles[triIndex * 3 + 1]] *= 0.9f;
        vertices[triangles[triIndex * 3 + 2]] *= 0.9f;

        mesh.vertices = vertices;

        filter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
    }
}
