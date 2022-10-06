using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Transform playerTrans;
    Rigidbody2D playerRig;
    CircleCollider2D playerCol;
    float originalPosition;
    const float maxHeight = 2.5f;
    float energy;
    void Awake() {
        playerTrans = gameObject.GetComponent<Transform>();
        playerRig = gameObject.GetComponent<Rigidbody2D>();
        playerCol = gameObject.GetComponent<CircleCollider2D>();

        initialize();
    }

    public void initialize() {
        originalPosition = -0.1f;
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
            //Debug.Log("Pressed Space");
            float upSpeed = Physics2D.gravity.magnitude * getUpMulti();
            playerRig.AddForce(new Vector3(0f, upSpeed, 0f), ForceMode2D.Force);
            //Debug.Log(SpeedMul);
        }
    }

    /// <summary>
    /// Determines acceleration curve
    /// </summary>
    /// <returns></returns>
    private float getUpMulti() {
        float portionY = (playerTrans.localPosition.y - originalPosition) / maxHeight;

        return (- portionY * portionY + 1) * 4f;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Black") {
            Debug.Log("Defeated");
            GameObject.Find("Global Control").GetComponent<GlobalControl>().endGame();
        }
    }
}
