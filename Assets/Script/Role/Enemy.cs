using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Role {

    public override void NextAction()
    {
        StartCoroutine(AIController.NextAction());
        RoleControlEnd();
    }

    // Use this for initialization
    void Start () {
        name = "Enemy";
        type = RoleType.PrimaryEnemy;
        this.gameObject.AddComponent<EnemyAi>();
        this.SetAi(this.GetComponent<EnemyAi>());
         
    }

    // Update is called once per frame
    void Update () {
        UpdateHealth();
    }
}
