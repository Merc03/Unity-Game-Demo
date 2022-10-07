using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBlack : Particle {

    const float timeWait = 1.5f;
    float timeWaitRest;

    public float TimewaitRest {
        get {
            return timeWaitRest;
        }
        set {
            timeWaitRest = value;
            if(timeWaitRest < 0f) timeWaitRest = 0f;
        }
    }

    void Awake() {
        current = gameObject;
        TimewaitRest = TimewaitRest * Random.value;
    }

    void Update() {
        TimewaitRest -= Time.deltaTime;

        if(timeWaitRest <= 0f) {
            moveForward(player, 0.15f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log(other.name);

        switch(other.tag) {
            case "Black": {
                if(other.transform.position.x > trans.position.x) {
                    delete();
                }
                else {
                    trans.localScale *= 1.5f;
                }
                break;
            }

            case "White": {
                trans.localScale /= 1.5f;
                if(trans.localScale.x < 0.25f) {
                    delete();
                }
                break;
            }
        }
    }
}
