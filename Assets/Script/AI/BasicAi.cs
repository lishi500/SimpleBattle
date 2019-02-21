using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicAI : MonoBehaviour {

    public GameManager gameManager;
    public GameState gameState;
    public Role currentRole;
    public Skill nextSkill;

     void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameController.GetComponent<GameManager>();
        gameState = gameController.GetComponent<GameState>();
        currentRole = this.GetComponent<Role>();
    }
       
    public abstract void AIController();

    public IEnumerator NextAction() {
        AIController();

        yield return StartCoroutine(nextSkill.CastSkill());

        nextSkill = null;
    } 

}
