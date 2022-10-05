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
            Vector3 deltaPosition = GameObject.Find("Player").transform.position - trans.position;
            trans.position += deltaPosition / 4.0f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log(other.name);
        if(other.tag == "Black") {
            //Debug.Log("Triggered");
            if(other.transform.position.x > trans.position.x) {
                delete();
            }
            else {
                trans.localScale *= 1.2f;
            }
        }
    }
}
