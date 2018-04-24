using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : MonoBehaviour {

    public GameObject mainWave;
    public ParticleSystem sideEffect_0;
    public ParticleSystem sideEffect_1;
    public AudioSourceLoudnessTester m_audioTester;

    //public Material[] matList;
    //private int currMat = 0;
    //private float lastChange = 0;
    //public float changeColorCD = 5.0f;
    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        // lastChange += Time.deltaTime;

        if (mainWave)
        {
            mainWave.transform.localScale = 5 * Vector3.one * Mathf.Abs(Perlin.Noise(m_audioTester.clipLoudness));
        }
        if (sideEffect_0)
        {
            sideEffect_0.transform.Rotate(0, 0, Mathf.Abs(Perlin.Noise(m_audioTester.clipLoudness)));
            sideEffect_0.Emit((int)(50 * Mathf.Abs(Perlin.Noise(m_audioTester.clipLoudness))));
            sideEffect_0.startColor = new Color(255, 255 , 255 * Mathf.Abs(Perlin.Noise(m_audioTester.clipLoudness)));

            //if (m_audioTester.clipLoudness > .3 && lastChange - Time.timeSinceLevelLoad > changeColorCD)
            //{
            //    if (matList[currMat+1])
            //        currMat += 1;
            //    else
            //        currMat = 0;
            //    sideEffect_0.GetComponent<ParticleSystemRenderer>().material = matList[currMat + 1];
            //    lastChange = Time.timeSinceLevelLoad;
            //}
        }
        if (sideEffect_1)
            sideEffect_1.Emit((int)(100 * Mathf.Abs(Perlin.Noise(m_audioTester.clipLoudness))));
    }
}
