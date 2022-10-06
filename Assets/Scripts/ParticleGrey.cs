using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGrey : Particle
{
    void Awake() {
        current = gameObject;
        generate();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log(other.name);
        switch(other.tag) {
            case "Black": {
                delete();
                break;
            }

            case "White": {
                delete();
                break;
            }
        }
    }
}
