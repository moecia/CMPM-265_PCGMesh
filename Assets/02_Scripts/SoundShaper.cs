using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class SoundShaper : MonoBehaviour {

    public Vector3 size = Vector3.one;

    public float radius = 25.0f;

    public int cubeCout = 60;

    MeshCreator mc = new MeshCreator();

    public AudioSourceLoudnessTester m_audioTester;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        // one submesh for each face
        Vector3 center = new Vector3(0, 0, 0);

        mc.Clear(); // Clear internal lists and mesh

        for (int i = 0; i < cubeCout; i++)
        {
            // center.Set(i * size.x * (float)1.2 * Mathf.PI, 0, i * size.z * (float)1.2 * Mathf.PI);
            center.Set(Mathf.Sin((360/ cubeCout) * i * Mathf.PI/180) * radius, 0, Mathf.Cos((360 / cubeCout) * i * Mathf.PI / 180) * radius);
            CreateCube(center, i);
        }

        meshFilter.mesh = mc.CreateMesh();
    }

    void CreateCube(Vector3 center, int i)
    {
        Vector3 cubeSize = size * 0.15f;
        float heightInc = 0;
        if (m_audioTester)
        {
                heightInc = 10*Mathf.Abs(Perlin.Noise((i+1) * m_audioTester.clipLoudness/100));
 
        }
        // top of the cube
        // t0 is top left point
        Vector3 t0 = new Vector3(center.x + cubeSize.x, center.y + heightInc, center.z - cubeSize.z);
        Vector3 t1 = new Vector3(center.x - cubeSize.x, center.y + heightInc, center.z - cubeSize.z);
        Vector3 t2 = new Vector3(center.x - cubeSize.x, center.y + heightInc, center.z + cubeSize.z);
        Vector3 t3 = new Vector3(center.x + cubeSize.x, center.y + heightInc, center.z + cubeSize.z);
        // bottom of the cube
        Vector3 b0 = new Vector3(center.x + cubeSize.x, center.y - cubeSize.y, center.z - cubeSize.z);
        Vector3 b1 = new Vector3(center.x - cubeSize.x, center.y - cubeSize.y, center.z - cubeSize.z);
        Vector3 b2 = new Vector3(center.x - cubeSize.x, center.y - cubeSize.y, center.z + cubeSize.z);
        Vector3 b3 = new Vector3(center.x + cubeSize.x, center.y - cubeSize.y, center.z + cubeSize.z);

        // Top square
        mc.BuildTriangle(t0, t1, t2);
        mc.BuildTriangle(t0, t2, t3);

        // Bottom square
        mc.BuildTriangle(b2, b1, b0);
        mc.BuildTriangle(b3, b2, b0);

        // Back square
        mc.BuildTriangle(b0, t1, t0);
        mc.BuildTriangle(b0, b1, t1);

        mc.BuildTriangle(b1, t2, t1);
        mc.BuildTriangle(b1, b2, t2);

        mc.BuildTriangle(b2, t3, t2);
        mc.BuildTriangle(b2, b3, t3);

        mc.BuildTriangle(b3, t0, t3);
        mc.BuildTriangle(b3, b0, t0);
    }

}
