using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePink : Particle
{
    /// <summary>
    /// Current behavior in "Inactive", "Follow"
    /// </summary>
    string mode;
    private const int maxEffectCount = 3;
    private const float attackDistance = 0.5f;
    private const float scaleMul = 1.2f;
    private int _effectCount;
    private int effectCount {
        get {
            return _effectCount;
        }
        set {
            _effectCount = value;
            if(_effectCount >= maxEffectCount) {
                Invoke("changeModeFollow", 5f * Random.value);
            }
            if(_effectCount <= 0 && mode == "Follow") {
                delete();
            }
        }
    }

    private float followDistance;
    private bool isParentChanged;

    void Awake() {
        current = gameObject;

        generate();

        mode = "Inactive";
        effectCount = 0;
        followDistance = 3f * Random.value;
        isParentChanged = false;
    }

    void Update() {
        switch(mode) {
            case "Inactive": {
                break;
            }

            case "Follow": {
                if(!isParentChanged) {
                    trans.parent = GameObject.Find("Particle Fixed").transform;
                    isParentChanged = true;

                    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                }

                GameObject target = control.GetComponent<ParticleGlobalControl>().targetBlack;
                if(target != null
                && Vector3.Distance(target.transform.position, player.transform.position) <= attackDistance) {
                    Debug.Log("Effect");
                    trans.localScale /= scaleMul;
                    if(effectCount > 0) {
                        control.GetComponent<ParticleGlobalControl>().clear(trans.position,
                        Vector3.Distance(trans.position, target.transform.position) * 1.01f);
                    }
                    else {
                        control.GetComponent<ParticleGlobalControl>().clear(trans.position,
                        Vector3.Distance(trans.position, target.transform.position) * 1.5f) ;
                    }
                    effectCount -= 1;
                }

                if(player.transform.position.x - trans.position.x > followDistance) {
                     moveForward(player, 0.5f);
                }
                else {
                    moveForwardAxisY(player, 0.5f);
                }

                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && mode == "Inactive") {
            effectCount += 1;
            trans.localScale *= scaleMul;
        }
    }

    /// <summary>
    /// To use with Invoke()
    /// </summary>
    private void changeModeFollow() {
        mode = "Follow";
    }
}
