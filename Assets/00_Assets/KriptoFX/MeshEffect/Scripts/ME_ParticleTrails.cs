using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

public class ME_ParticleTrails : MonoBehaviour
{
    public GameObject TrailPrefab;

    private ParticleSystem ps;
    ParticleSystem.Particle[] particles;

    private Dictionary<uint, GameObject> hashTrails = new Dictionary<uint, GameObject>();

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    void OnEnable()
    {
        InvokeRepeating("ClearEmptyHashes", 1, 1);
    }

    void OnDisable()
    {
        CancelInvoke("ClearEmptyHashes");
    }

    void Update()
    {
        UpdateTrail();
    }

    void UpdateTrail()
    {
        int count = ps.GetParticles(particles);
        for (int i = 0; i < count; i++)
        {
            if (!hashTrails.ContainsKey(particles[i].randomSeed))
            {
                var go = Instantiate(TrailPrefab, transform.position, new Quaternion());
                go.hideFlags = HideFlags.HideInHierarchy;
                hashTrails.Add(particles[i].randomSeed, go);
                var trail = go.GetComponent<LineRenderer>();
                trail.widthMultiplier *= particles[i].startSize;
            }
            else
            {
                var go = hashTrails[particles[i].randomSeed];
                if (go != null)
                {
                    var trail = go.GetComponent<LineRenderer>();
                    trail.startColor *= particles[i].GetCurrentColor(ps);
                    trail.endColor *= particles[i].GetCurrentColor(ps);

                    if (ps.main.simulationSpace == ParticleSystemSimulationSpace.World)
                        go.transform.position = particles[i].position;
                    if (ps.main.simulationSpace == ParticleSystemSimulationSpace.Local)
                        go.transform.position = ps.transform.TransformPoint(particles[i].position);

                }
            }
        }
    }

    void ClearEmptyHashes()
    {
        hashTrails = hashTrails.Where(h => h.Value != null).ToDictionary(h => h.Key, h => h.Value);
    }
}
