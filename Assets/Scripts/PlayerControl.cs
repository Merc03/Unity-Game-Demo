using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Transform playerTrans;
    Rigidbody2D playerRig;
    CircleCollider2D playerCol;
    const float upSpeed = 3f;
    float energy;
    void Awake() {
        playerTrans = gameObject.GetComponent<Transform>();
        playerRig = gameObject.GetComponent<Rigidbody2D>();
        playerCol = gameObject.GetComponent<CircleCollider2D>();

        initialize();
    }

    public void initialize() {
        transform.GetComponentInChildren<PlayerDetection>().initialize();
    }

    void Update() {
        playerTrans.localRotation = Quaternion.Euler(new Vector3(0f, 0f, playerRig.velocity.y * 10f));
        //Debug.Log(playerRig.velocity.y);
    }

    void FixedUpdate() {
        //playerRig.velocity = new Vector3(0f, 0f, 0f);
        //Debug.Log("Updating");
        playerTrans.localRotation = Quaternion.Euler(playerRig.velocity);
        if(Input.GetKey(KeyCode.Space)) {
            if(transform.position.y < GlobalSettings.maxHeight) {
                Vector3 Vel = playerRig.velocity;
                Vel.y = upSpeed;
                playerRig.velocity = Vel;
            }
            else {
                playerRig.velocity = Vector3.zero;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Black") {
            Debug.Log("Defeated");
            GameObject.Find("Global Control").GetComponent<GlobalControl>().endGame();
        }
    }
}
