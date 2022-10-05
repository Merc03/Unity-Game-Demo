using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGlobalControl : MonoBehaviour
{
    /// <summary>
    /// Start a new game and generate particle
    /// </summary>
    public void initialize() {
        clearAll();

        // to be rewrite

        // Yellow particles
        for(int i = 0; i < GlobalSettings.maxParticle; ++i) {
            GameObject cur = (GameObject) Resources.Load("Prefabs/Particle Yellow");
            Instantiate(cur);
        }

        for(int i = 0; i < 5; ++i) {
            GameObject cur = (GameObject) Resources.Load("Prefabs/Particle Blue");
            Instantiate(cur);
        }
    }

    /// <summary>
    /// (Unsafe, to be tested !) Clear all particles
    /// </summary>
    public void clearAll() {
        GameObject[] fathers = {gameObject.transform.GetChild(0).gameObject, gameObject.transform.GetChild(1).gameObject};
        foreach(GameObject father in fathers) {
            int childCount = father.transform.childCount;
            for(int i = 0; i < childCount; ++i) {
                Destroy(father.transform.GetChild(i).gameObject);
            }
        }
    }

    public void generateParticle(Vector3 targetPosition) {
        //Debug.Log("To generate black particle at" + other.transform.position.x + other.transform.position.y);
        GameObject cur = (GameObject) Resources.Load("Prefabs/Particle Black");
        cur = Instantiate(cur);
        //if(cur == null) Debug.Log("Initialization Failed");
        cur.GetComponent<ParticleBlack>().generate(targetPosition);
    }
}

public class Particle : MonoBehaviour
{
    public GameObject current;
    public Transform trans;
    public SpriteRenderer sRender;

    /// <summary>
    /// Automatically generate the particle outside the camera with particle color
    /// </summary>
    public void generate() {
        trans = current.GetComponent<Transform>();
        sRender = GetComponent<SpriteRenderer>();
        trans.parent = GameObject.Find("Particle Rotation").transform;

        do {
            float height = Random.value * 3;
            trans.localPosition = (new Vector3(0f, GlobalSettings.RotationLocationY - height, 0f));
            trans.RotateAround(trans.parent.localPosition, new Vector3(0f, 0f, 1f), GlobalSettings.getDegree() * Random.value);
        } while(isInCamera());

    }

    /// <summary>
    /// Manually generate the particle at given position
    /// </summary>
    public void generate(Vector3 targetPosition) {
        trans = current.GetComponent<Transform>();
        sRender = GetComponent<SpriteRenderer>();
        trans.parent = GameObject.Find("Particle Fixed").transform;

        trans.position = targetPosition;
    }

    public void delete() {
        Destroy(current);
        Destroy(this);
    }

    public bool isInCamera() {
        // to be rewrite
        return !(trans.position.y <= GlobalSettings.RotationLocationY / 2f);
    }
}