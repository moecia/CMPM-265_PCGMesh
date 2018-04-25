using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class CubeMaker : MonoBehaviour
{
    public Vector3 size = Vector3.one;

    MeshCreator mc = new MeshCreator();

    public bool canBeAnimated = false;
    public bool animateVFX = false;

    private float timer = 0;

    public GameObject[,] vfxList = new GameObject[50,50];
    public GameObject vfxEffect;

    [HideInInspector]
    public int row = 20;
    [HideInInspector]
    public int col = 20;
    [HideInInspector]
    public float sizeMultiplier = .5f;
        [HideInInspector]
    public float oriHeight = 1;
    [HideInInspector]
    public int animateSpeed = 10;
    [HideInInspector]
    public float distance = 1.2f;

    private void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                GameObject temp = Instantiate(vfxEffect, new Vector3(j * size.x * (float)distance, 0, i * size.z * (float)distance), Quaternion.Euler(-90, 0, 0));
                temp.name = "VFX_Row_" + i + "_Col_" + j;
                temp.transform.parent = GameObject.Find("VFXList").transform;
                vfxList[i, j] = temp;

                if (vfxList[i, j].GetComponent<ParticleSystem>())
                    vfxList[i, j].GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        // one submesh for each face
        Vector3 center = new Vector3(0, 0, 0);

        mc.Clear(); // Clear internal lists and mesh



        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                center.Set(j * size.x * (float)distance, 
                    0, 
                    i * size.z * (float)distance);

                vfxList[i, j].transform.position = new Vector3(j * size.x * (float)distance, 0, i * size.z * (float)distance);
                CreateCube(center, center.x, center.z, i, j);
            }
        }

        if(!animateVFX)
            meshFilter.mesh = mc.CreateMesh();
    }

    void CreateCube(Vector3 center, float x, float z, int row, int col)
    {
        Vector3 cubeSize = size * sizeMultiplier;
        
        float heightInc = 0;

        if (!canBeAnimated)
            heightInc = .9f * Perlin.Noise(x/10, z/10);
        else
            heightInc = .9f * Perlin.Noise(x+1 , (z+1) * Time.realtimeSinceStartup/animateSpeed);

        // heightInc = Mathf.Clamp(Perlin.Noise(x , z * Time.realtimeSinceStartup), 0.05f, 0.2f);


        if (animateVFX)
        {
            if (heightInc > 0.3f)
            {
                if (vfxList[row, col].GetComponent<ParticleSystem>())
                {
                    vfxList[row, col].GetComponent<ParticleSystem>().Play();
                    vfxList[row, col].GetComponent<ParticleSystem>().startSpeed = heightInc * 10;
                }
            }
            else
            {
                if (vfxList[row, col].GetComponent<ParticleSystem>())
                    vfxList[row, col].GetComponent<ParticleSystem>().Stop();
            }
        }

            // top of the cube
            // t0 is top left point
            Vector3 t0 = new Vector3(center.x + cubeSize.x, center.y + heightInc * oriHeight, center.z - cubeSize.z);
            Vector3 t1 = new Vector3(center.x - cubeSize.x, center.y + heightInc * oriHeight, center.z - cubeSize.z);
            Vector3 t2 = new Vector3(center.x - cubeSize.x, center.y + heightInc * oriHeight, center.z + cubeSize.z);
            Vector3 t3 = new Vector3(center.x + cubeSize.x, center.y + heightInc * oriHeight, center.z + cubeSize.z);
            // bottom of the cube
            Vector3 b0 = new Vector3(center.x + cubeSize.x, center.y - cubeSize.y, center.z - cubeSize.z);
            Vector3 b1 = new Vector3(center.x - cubeSize.x, center.y - cubeSize.y, center.z - cubeSize.z);
            Vector3 b2 = new Vector3(center.x - cubeSize.x, center.y - cubeSize.y, center.z + cubeSize.z);
            Vector3 b3 = new Vector3(center.x + cubeSize.x, center.y - cubeSize.y, center.z + cubeSize.z);

        if (center.y + heightInc * oriHeight < 0)
        {
            t0 = new Vector3(center.x + cubeSize.x, center.y, center.z - cubeSize.z);
            t1 = new Vector3(center.x - cubeSize.x, center.y, center.z - cubeSize.z);
            t2 = new Vector3(center.x - cubeSize.x, center.y, center.z + cubeSize.z);
            t3 = new Vector3(center.x + cubeSize.x, center.y, center.z + cubeSize.z);

            b0 = new Vector3(center.x + cubeSize.x, center.y - cubeSize.y + heightInc * oriHeight, center.z - cubeSize.z);
            b1 = new Vector3(center.x - cubeSize.x, center.y - cubeSize.y + heightInc * oriHeight, center.z - cubeSize.z);
            b2 = new Vector3(center.x - cubeSize.x, center.y - cubeSize.y + heightInc * oriHeight, center.z + cubeSize.z);
            b3 = new Vector3(center.x + cubeSize.x, center.y - cubeSize.y + heightInc * oriHeight, center.z + cubeSize.z);

        }
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