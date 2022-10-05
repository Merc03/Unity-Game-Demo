using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)) {
            Debug.Log("New Game Started");
            GameObject.Find("Particle Global Control").GetComponent<ParticleGlobalControl>().initialize();
        }
    }
}
