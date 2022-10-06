using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGlobalControl : MonoBehaviour
{
    private GameObject player;
    private GameObject _targetBlack = null;
    public GameObject targetBlack {
        get {
            return _targetBlack;
        }
        set {
            _targetBlack = value;
        }
    }

    void Awake() {
        player = GameObject.Find("Player");
    }

    void Update() {
        targetBlack = null;
        Transform father = gameObject.transform.Find("Particle Fixed");
        int childCount = father.childCount;
        for(int i = 0; i < childCount; ++i) {
            GameObject temp = father.GetChild(i).gameObject;
            if(temp.tag == "Black") {
                if(targetBlack == null) {
                    targetBlack = temp;
                }
                else {
                    float disOld = Vector3.Distance(player.transform.position, targetBlack.transform.position);
                    float disNew = Vector3.Distance(player.transform.position, temp.transform.position);
                    if(disNew < disOld) {
                        targetBlack = temp;
                    }
                }
            }
        }
    }

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

        for(int i = 0; i < 5; ++i) {
            GameObject cur = (GameObject) Resources.Load("Prefabs/Particle White");
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
    protected GameObject player, current, control;
    protected Transform trans;
    protected SpriteRenderer sRender;

    /// <summary>
    /// Automatically generate the particle outside the camera with particle color
    /// </summary>
    public void generate() {
        player = GameObject.Find("Player");
        control = GameObject.Find("Particle Global Control");
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
        player = GameObject.Find("Player");
        control = GameObject.Find("Particle Global Control");
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

    /// <summary>
    /// Whether the particle has passed the axis where player is in
    /// </summary>
    /// <returns></returns>
    public bool isPastPlayer() {
        return sRender.isVisible && trans.position.x < -0.1f;
    }

    public void moveForward(GameObject other, float speedMul) {
        Vector3 distance = other.transform.position - trans.position;
        trans.position += distance * speedMul * Time.deltaTime;
    }

    public void moveForwardAxisY(GameObject other, float speedMul) {
        Vector3 distance = other.transform.position - trans.position;
        distance.x = 0f;
        trans.position += distance * speedMul * Time.deltaTime;
    }
}