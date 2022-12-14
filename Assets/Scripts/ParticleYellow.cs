using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleYellow : Particle
{
    const float timeTurnBlack = 3f;

    private float _timeTurnRest;
    private bool isPastScreen;
    float timeTurnRest {
        get {
            return _timeTurnRest;
        }
        set {
            _timeTurnRest = value;
            if(_timeTurnRest < 0f) _timeTurnRest = 0f;
        }
    }

    void Awake() {
        current = gameObject;

        generate();

        timeTurnRest = timeTurnBlack * (1f + Random.value);
        isPastScreen = false;
    }

    void Update() {
        isPastScreen |= sRender.isVisible;

        if(!sRender.isVisible && isPastScreen) {
            GameObject.Find("Particle Global Control").GetComponent<ParticleGlobalControl>().generateParticle(trans.position);
            delete();
        }

        if(timeTurnRest <= 0f) {
            GameObject.Find("Particle Global Control").GetComponent<ParticleGlobalControl>().generateParticle(trans.position);
            delete();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            delete();
        }
    }
}
