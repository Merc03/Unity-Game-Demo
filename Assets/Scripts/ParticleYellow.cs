using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleYellow : Particle
{
    void Awake() {
        current = gameObject;

        generate();
    }

    void Update() {

    }

    private void OnTriggerStay2D(Collider2D other) {
        //to be rewrite

        generate();
        //delete();
    }
}
