using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWhite : Particle
{
    /// <summary>
    /// Current behavior in "Inactive", "Follow"
    /// </summary>
    string mode;

    const float timeSwitchFollow = 3f;

    bool isParentChanged;

    private float _timeSwitchRest;
    float timeSwitchRest {
        get {
            return _timeSwitchRest;
        }
        set {
            _timeSwitchRest = value;
            if(_timeSwitchRest < 0f) _timeSwitchRest = 0f;
        }
    }

    float followDistance;
    private bool isPastScreen;

    void Awake()
    {
        current = gameObject;
        generate();

        mode = "Inactive";
        isParentChanged = false;
        timeSwitchRest = timeSwitchFollow;
        followDistance = 3f * Random.value;
        isParentChanged = false;
    }

    void Update() {
        switch(mode) {
            case "Inactive": {
                isPastScreen |= sRender.isVisible;

                if(!sRender.isVisible && isPastScreen) {
                    delete();
                }

                break;
            }

            case "Follow": {
                if(timeSwitchRest <= 0) { // following
                    if(!isParentChanged) {
                        trans.parent = GameObject.Find("Particle Fixed").transform;
                        isParentChanged = true;

                        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                    }
                    else {
                        GameObject target = control.GetComponent<ParticleGlobalControl>().targetBlack;
                        if(target != null) {
                            moveForward(target, 3f);
                        }
                        else {
                            if(player.transform.position.x - trans.position.x > followDistance) {
                                moveForward(player, 0.5f);
                            }
                            else {
                                moveForwardAxisY(player, 0.5f);
                            }
                        }
                    }
                }
                else { // to be switched
                    timeSwitchRest -= Time.deltaTime;
                }
                break;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.tag) {
            case "Player": {
                if(mode == "Inactive") {
                    trans.localScale *= 1.5f;
                }
                mode = "Follow";
                break;
            }

            case "Black": {
                delete();
                break;
            }
        }
    }

}
