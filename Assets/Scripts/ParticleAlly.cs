using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAlly : MonoBehaviour
{
    Transform trans;
    SpriteRenderer sRender;

    bool isRefreshed = false;

    void Awake() {
        trans = GetComponent<Transform>();

        sRender = GetComponent<SpriteRenderer>();
        sRender.sortingLayerName = "Objects";
        sRender.sortingOrder = 2;

        trans.parent = GameObject.Find("Particle Allies").transform;

        refresh();
    }

    void Update() {
        //Debug.Log(trans.position.y);
        if(trans.position.y <= globalSettings.RotationLocationY) {
            sRender.enabled = true;
            if(!isRefreshed) {
                refresh();
            }
        }
        else {
            if(sRender.isVisible) {
                isRefreshed = false;
            }
        }
    }

    /// <summary>
    /// Regenerate the particle
    /// </summary>
    void refresh() {
        isRefreshed = true;

        float height = Random.value * 3;
        trans.localPosition = (new Vector3(0f, globalSettings.RotationLocationY - height, 0f));
        trans.RotateAround(trans.parent.localPosition, new Vector3(0f, 0f, 1f), globalSettings.getDegree() * Random.value);
        //trans.localRotation = Quaternion.Euler(new Vector3(0f, 0f, globalSettings.getDegree() * Random.value));

        sRender.enabled = false;
        //Debug.Log("Refreshed");
    }
}
