using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SphereMaker : MonoBehaviour
{
    public float radius = 1f;

    [SerializeField]
    int m_longtitude = 24;
    [SerializeField]
    int m_latitude = 16;
    [HideInInspector]
    public int animateSpeed = 100;

    public bool canBeAnimated = true;


    private void Update()
    {
        BuildSphere();
    }

    private void BuildSphere()
    {
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        mesh.Clear();

        #region Vertices
        Vector3[] vertices = new Vector3[(m_longtitude + 1) * m_latitude + 2];
        float m_PI = Mathf.PI;
        float m_2PI = m_PI * 2f;

        vertices[0] = Vector3.up * radius;
        for (int lat = 0; lat < m_latitude; lat++)
        {
            float a1 = m_PI * (float)(lat + 1) / (m_latitude + 1);
            float sin1 = Mathf.Sin(a1);
            float cos1 = Mathf.Cos(a1);

            for (int lon = 0; lon <= m_longtitude; lon++)
            {
                float a2 = m_2PI * (float)(lon == m_longtitude ? 0 : lon) / m_longtitude;
                float sin2 = Mathf.Sin(a2);
                float cos2 = Mathf.Cos(a2);

                float noiseR = radius;
                if (canBeAnimated)
                    noiseR = radius + Mathf.Abs(Perlin.Noise((float)lat * Time.time / animateSpeed, (float)lon * Time.time / animateSpeed));

                vertices[lon + lat * (m_longtitude + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * noiseR;
            }
        }
        vertices[vertices.Length - 1] = Vector3.up * -radius;
        #endregion

        #region Normales		
        Vector3[] normales = new Vector3[vertices.Length];
        for (int n = 0; n < vertices.Length; n++)
            normales[n] = vertices[n].normalized;
        #endregion

        #region UVs
        Vector2[] uvs = new Vector2[vertices.Length];
        uvs[0] = Vector2.up;
        uvs[uvs.Length - 1] = Vector2.zero;
        for (int lat = 0; lat < m_latitude; lat++)
            for (int lon = 0; lon <= m_longtitude; lon++)
                uvs[lon + lat * (m_longtitude + 1) + 1] = new Vector2((float)lon / m_longtitude, 1f - (float)(lat + 1) / (m_latitude + 1));
        #endregion

        #region Triangles
        int nbFaces = vertices.Length;
        int nbTriangles = nbFaces * 2;
        int nbIndexes = nbTriangles * 3;
        int[] triangles = new int[nbIndexes];

        //Top Cap
        int i = 0;
        for (int lon = 0; lon < m_longtitude; lon++)
        {
            triangles[i++] = lon + 2;
            triangles[i++] = lon + 1;
            triangles[i++] = 0;
        }

        //Middle
        for (int lat = 0; lat < m_latitude - 1; lat++)
        {
            for (int lon = 0; lon < m_longtitude; lon++)
            {
                int current = lon + lat * (m_longtitude + 1) + 1;
                int next = current + m_longtitude + 1;

                triangles[i++] = current;
                triangles[i++] = current + 1;
                triangles[i++] = next + 1;

                triangles[i++] = current;
                triangles[i++] = next + 1;
                triangles[i++] = next;
            }
        }

        //Bottom Cap
        for (int lon = 0; lon < m_longtitude; lon++)
        {
            triangles[i++] = vertices.Length - 1;
            triangles[i++] = vertices.Length - (lon + 2) - 1;
            triangles[i++] = vertices.Length - (lon + 1) - 1;
        }
        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;
    }
}
