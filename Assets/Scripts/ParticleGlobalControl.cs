using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGlobalControl : MonoBehaviour
{
    void Awake() {
        initialize();
    }

    /// <summary>
    /// Start a new game and generate particle
    /// </summary>
    public void initialize() {
        // clearAll();

        // to be rewrite

        // Yellow particles
        for(int i = 0; i < GlobalSettings.maxParticle; ++i) {
            GameObject cur = (GameObject) Resources.Load("Prefabs/Particle Yellow");
            Instantiate(cur);
            Debug.Log("Successfully initialized");
        }

        for(int i = 0; i < 5; ++i) {
            GameObject cur = (GameObject) Resources.Load("Prefabs/Particle Blue");
            Instantiate(cur);
            Debug.Log("Successfully initialized");
        }
    }

    /// <summary>
    /// (Unsafe, to be tested !) Clear all particles
    /// </summary>
    public void clearAll() {
        string[] toFind = {"Particle Yellow", "Particle Blue", "Particle White", "Particle Black"};
        foreach(string cur in toFind) {
            GameObject tar = GameObject.Find(cur + "(Clone)");;
            while(tar != null) {
                Debug.Log("Successfully removed");
                tar.SendMessage("delete");
                tar = GameObject.Find(cur + "(Clone)");;
            }
        }
    }
}

public class Particle : MonoBehaviour
{
    public GameObject current;
    public Transform trans;
    public SpriteRenderer sRender;

    /// <summary>
    /// Generate the particle outside the camera with particle color
    /// </summary>
    public virtual void generate() {
        trans = current.GetComponent<Transform>();
        trans.parent = GameObject.Find("Particle Rotation").transform;

        do {
            float height = Random.value * 3;
            trans.localPosition = (new Vector3(0f, GlobalSettings.RotationLocationY - height, 0f));
            trans.RotateAround(trans.parent.localPosition, new Vector3(0f, 0f, 1f), GlobalSettings.getDegree() * Random.value);
        } while(isInCamera());

        sRender = GetComponent<SpriteRenderer>();
        sRender.sortingLayerName = "Objects";
        sRender.sortingOrder = 2;
    }

    /// <summary>
    /// Generate the particle at given position
    /// </summary>
    public virtual void generate(float positionX, float positonY) {

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