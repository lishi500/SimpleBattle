using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : BasicAI {

    public override void AIController()
    {
        nextSkill = SkillUtils.Instance.CreateSkill("AttackSkill", currentRole, gameState.player, null);

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
