using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator
{

    private List<Vector3> vertices = new List<Vector3>();

    private List<Vector3> normals = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();

    private List<int> triangleIndices = new List<int>();

    private Mesh mesh = null;


    public MeshCreator()
    {
        // No initialization required in constuctor
    }

    public void BuildTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
    {
        Vector3 normal = Vector3.Cross(vertex1 - vertex0, vertex2 - vertex0).normalized;

        BuildTriangle(vertex0, vertex1, vertex2, normal);
    }

    public void BuildTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, Vector3 normal)
    {
        int v0Index = vertices.Count;
        int v1Index = vertices.Count + 1;
        int v2Index = vertices.Count + 2;

        // Put vertex data into vertices array
        vertices.Add(vertex0);
        vertices.Add(vertex1);
        vertices.Add(vertex2);

        // Use the same normal for all vertices (i.e., a surface normal)
        // Could change function signature to pass in 3 normals if needed
        normals.Add(normal);
        normals.Add(normal);
        normals.Add(normal);

        // Use standard uv coordinates
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(1, 1));

        // Add integer pointers to vertices into triangles list
        triangleIndices.Add(v0Index);
        triangleIndices.Add(v1Index);
        triangleIndices.Add(v2Index);

    }

    public Mesh CreateMesh()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
        }

        mesh.vertices = vertices.ToArray();

        mesh.normals = normals.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.triangles = triangleIndices.ToArray();

        return mesh;
    }

    public void Clear()
    {
        if (mesh != null)
        {
            mesh.Clear();
        }

        vertices.Clear();

        normals.Clear();

        uvs.Clear();

        triangleIndices.Clear();

    }

}