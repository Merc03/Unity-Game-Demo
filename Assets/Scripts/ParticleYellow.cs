using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleYellow : Particle
{
    const float timeTurnBlack = 0.5f;

    float timeTurnRest;

    public float TimeTurnRest {
        get {
            return timeTurnRest;
        }
        set {
            timeTurnRest = value;
            if(timeTurnRest < 0f) timeTurnRest = 0f;
        }
    }

    void Awake() {
        current = gameObject;

        generate();

        TimeTurnRest = timeTurnBlack * (1f + Random.value);
    }

    void Update() {
        if(sRender.isVisible && trans.position.x < -0.1f) {
            TimeTurnRest -= Time.deltaTime;
        }

        if(TimeTurnRest <= 0f) {
            GameObject.Find("Particle Global Control").GetComponent<ParticleGlobalControl>().generateParticle(trans.position);
            delete();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            generate();
        }
    }
}
