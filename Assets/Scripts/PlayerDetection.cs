using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    Transform trans;
    CircleCollider2D col;
    float radius;
    float energy;
    const float energyGainYellow = 0.1f;
    const float energyGainGround = 0.05f;
    const float energyFade = 0.025f;
    const float maxEnergy = 1.5f;

    void Awake() {
        trans = gameObject.GetComponent<Transform>();
        col = gameObject.GetComponent<CircleCollider2D>();
        Energy = 0f;
    }

    void Update() {
        Energy -= energyFade * Time.deltaTime;
        radius = 1.0f + Energy * Energy;

        // keep transform and collider at the same size
        trans.localScale = new Vector3(radius, radius, 1f);
        // col.radius += 0.001f;

        //Debug.Log(Energy);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.tag) {
            case "Yellow": {
                Energy += energyGainYellow;
                break;
            }

            case "Blue": {

                break;
            }

            case "Ground": {
                break;
            }

            default: {
                Debug.Log("Unknow Trigger: " + other.name);
                break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag != "Ground") {
            //Debug.Log("Unknown Trigger: " + other.name);
            return;
        }

        Energy += energyGainGround * Time.deltaTime;
    }

    /// <summary>
    /// Automatically maintains energy
    /// </summary>
    /// <value></value>
    public float Energy {
        get {
            return energy;
        }
        set {
            energy = value;
            if(energy < 0f) energy = 0f;
            if(energy > maxEnergy) energy = maxEnergy;
        }
    }
}
