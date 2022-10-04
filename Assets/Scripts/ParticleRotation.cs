using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotation : MonoBehaviour
{
    Transform trans;

    void Awake()
    {
        trans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        trans.localRotation = Quaternion.Euler(new Vector3(0f, 0f, GlobalSettings.getDegree()));
    }
}
