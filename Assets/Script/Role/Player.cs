using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Role {

    public override void Awake()
    {
        base.Awake();
        name = "PlayerA";
        type = RoleType.Player;
    }

    public override void NextAction()
    {
    }


    // Use this for initialization
    void Start () {
        
	}

    // Update is called once per frame
    void Update () {
        UpdateHealth();
    }
}
