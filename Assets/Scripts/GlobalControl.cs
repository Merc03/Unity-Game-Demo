using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    GameObject notice;
    bool isFirstGame = true;

    void Awake() {
        notice = GameObject.Find("Notice");
        GameObject.Find("Notice").SetActive(false);
    }

    void Start() {
        newGame();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)) {
            notice.SetActive(false);
            newGame();
        }
    }

    public void newGame() {
        Debug.Log("New Game Started");

        if(isFirstGame) {
                Destroy(GameObject.Find("Instruction"), 5f);
            }

        GlobalSettings.initialize();

        GameObject.Find("Particle Global Control").GetComponent<ParticleGlobalControl>().initialize();
        GameObject.Find("Player").GetComponent<PlayerControl>().initialize();
    }

    public void endGame() {
        notice.SetActive(true);
    }
}
