using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRotation : MonoBehaviour
{
    [SerializeField] Transform ground;

    void Awake()
    {
        //ground.localRotation = Quaternion.Euler(0f, 0f, getDegree());
    }

    // Update is called once per frame
    void Update()
    {
        ground.localRotation = Quaternion.Euler(0f, 0f, GlobalSettings.getDegree());
    }
}
