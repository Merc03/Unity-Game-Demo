using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : globalSettings
{
    Transform playerTrans;
    Rigidbody2D playerRig;

    float originalPosition, maxHeight;

    void Awake() {
        playerTrans = gameObject.GetComponent<Transform>();
        playerRig = gameObject.GetComponent<Rigidbody2D>();

        originalPosition = playerTrans.localPosition.y;
        maxHeight = 2.5f;
    }

    void Update() {
        playerTrans.localRotation = Quaternion.Euler(new Vector3(0f, 0f, playerRig.velocity.y * 10f));
        //Debug.Log(playerRig.velocity.y);
    }

    void FixedUpdate() {
        //playerRig.velocity = new Vector3(0f, 0f, 0f);
        playerTrans.localRotation = Quaternion.Euler(playerRig.velocity);
        if(Input.GetKey(KeyCode.Space)) {
            float upSpeed = Physics2D.gravity.magnitude * getUpMulti();
            playerRig.AddForce(new Vector3(0f, upSpeed, 0f), ForceMode2D.Force);
            //Debug.Log(SpeedMul);
        }    
    }

    /// <summary>
    /// Determines acceleration curve
    /// </summary>
    /// <returns></returns>
    float getUpMulti() {
        float portionY = (playerTrans.localPosition.y - originalPosition) / maxHeight;
        /*if(portionY > 1) {
            return 1f;
        }
        else {
            return Mathf.Sqrt(- portionY + 1f) + 1;
        }*/
        return (- portionY * portionY + 1) * 2f;
    }
}
