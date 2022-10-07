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

    public Dictionary<string, int> particleCount = new Dictionary<string, int>();
    private Dictionary<string, int> maxParticleCount = new Dictionary<string, int>();
    private float timeCount, maxTimeCount;
    void Awake() {
        player = GameObject.Find("Player");

        maxParticleCount["Yellow"] = 0;
        maxParticleCount["Blue"] = 0;
        maxParticleCount["White"] = 0;
        maxParticleCount["Pink"] = 0;

        particleCount["Yellow"] = 0;
        particleCount["Blue"] = 0;
        particleCount["White"] = 0;
        particleCount["Pink"] = 0;
    }

    void Update() {
        updateTargetBlack();

        timeCount += Time.deltaTime;
        if(timeCount >= maxTimeCount) {
            //Debug.Log("Time: " + timeCount);
            timeCount = 0f;
            //maxTimeCount = myMath.RandomGaussian(0f, 1f) * 5 + 1f;
            maxTimeCount = GlobalSettings.getTimeCount();

            string[] tags = {"Yellow", "Blue", "White", "Pink"};
            foreach(string tag in tags) {
                if(maxParticleCount[tag] - particleCount[tag] > 0) {
                    if(Random.value > 0.75f) {
                        continue;
                    }
                    GameObject cur = (GameObject) Resources.Load("Prefabs/Particle " + tag);
                    Instantiate(cur);
                    particleCount[tag] += 1;
                    break;
                }
            }
        }
        //Debug.Log(particleCount.Values);
    }

    /// <summary>
    /// Start a new game and generate particle
    /// </summary>
    public void initialize() {
        clearAll();

        maxParticleCount["Yellow"] = 15;
        maxParticleCount["Blue"] = 5;
        maxParticleCount["White"] = 3;
        maxParticleCount["Pink"] = 1;

        particleCount["Yellow"] = 0;
        particleCount["Blue"] = 0;
        particleCount["White"] = 0;
        particleCount["Pink"] = 0;
        particleCount["Black"] = 0;

        // to be rewrite

        /* for(int i = 0; i < GlobalSettings.maxParticle; ++i) {
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

        for(int i = 0; i < 1; ++i) {
            GameObject cur = (GameObject) Resources.Load("Prefabs/Particle Pink");
            Instantiate(cur);
        } */

        timeCount = 0f;
    }

    /// <summary>
    /// Clear all particles
    /// </summary>
    public void clearAll() {
        GameObject[] fathers = {gameObject.transform.GetChild(0).gameObject, gameObject.transform.GetChild(1).gameObject};
        foreach(GameObject father in fathers) {
            int childCount = father.transform.childCount;
            for(int i = 0; i < childCount; ++i) {
                GameObject child = father.transform.GetChild(i).gameObject;
                particleCount[child.tag] -= 1;
                Destroy(child);
            }
        }
    }

    /// <summary>
    /// Generate a black particle in given position
    /// </summary>
    /// <param name="targetPosition"></param>
    public void generateParticle(Vector3 targetPosition) {
        if(Random.value > 0.5f) {
            GameObject cur = (GameObject) Resources.Load("Prefabs/Particle Black");
            cur = Instantiate(cur);

            cur.GetComponent<ParticleBlack>().generate(targetPosition);
        }
        else {
             GameObject cur = (GameObject) Resources.Load("Prefabs/Particle Grey");
            Instantiate(cur);
        }
    }

    /// <summary>
    /// Clear all black particles around given location
    /// </summary>
    public void clear(Vector3 center, float radius) {
        GameObject[] fathers = {gameObject.transform.GetChild(0).gameObject, gameObject.transform.GetChild(1).gameObject};
        foreach(GameObject father in fathers) {
            int childCount = father.transform.childCount;
            for(int i = 0; i < childCount; ++i) {
                Transform child = father.transform.GetChild(i);
                if(child.gameObject.tag == "Black"
                && Vector3.Distance(child.position, center) <= radius) {
                    particleCount[child.gameObject.tag] -= 1;
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public void updateTargetBlack() {
        targetBlack = null;
        GameObject[] fathersObj = {gameObject.transform.GetChild(0).gameObject, gameObject.transform.GetChild(1).gameObject};
        foreach(GameObject fatherObj in fathersObj) {
            Transform father = fatherObj.transform;
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
    }

}


public class Particle : MonoBehaviour
{
    protected GameObject player, current, control;
    protected Transform trans;
    protected SpriteRenderer sRender;

    /// <summary>
    /// Automatically generate the particle outside the camera
    /// </summary>
    public void generate() {
        player = GameObject.Find("Player");
        control = GameObject.Find("Particle Global Control");
        trans = current.GetComponent<Transform>();
        sRender = GetComponent<SpriteRenderer>();
        trans.parent = GameObject.Find("Particle Rotation").transform;

        float radius = GlobalSettings.nextHeight() + GlobalSettings.RotationRadius;
        trans.position = trans.parent.position + new Vector3(radius * Mathf.Sin(0.25f * Mathf.PI), radius * (Mathf.Cos(0.25f * Mathf.PI)), 0f);
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
        control.GetComponent<ParticleGlobalControl>().particleCount[current.tag] -= 1;
        Destroy(current);
        Destroy(this);
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