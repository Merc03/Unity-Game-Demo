using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAllies : MonoBehaviour
{
    const int maxParticle = 30;

    GameObject[] particle;

    Transform trans;

    void Awake()
    {
        trans = GameObject.Find("Particle Allies").transform;

        particle = new GameObject[maxParticle];
        for(int i = 0; i < maxParticle; ++i) {
            particle[i] = (GameObject) Resources.Load("Prefabs/Particle Ally");

            particle[i] = Instantiate(particle[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        trans.localRotation = Quaternion.Euler(new Vector3(0f, 0f, globalSettings.getDegree()));
    }
}
