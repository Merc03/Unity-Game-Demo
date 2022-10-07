using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBlue : Particle
{
    private bool isPastScreen;
    void Awake() {
        current = gameObject;

        generate();

        isPastScreen = false;
    }

    void Update() {
        isPastScreen |= sRender.isVisible;

        if(!sRender.isVisible && isPastScreen) {
            delete();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        //to be rewrite

        if(other.tag == "Player") {
            delete();
        }
    }
}
