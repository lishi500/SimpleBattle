using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAction : Action {

    public override void Init(Role from, Role to, ActionConfig config)
    {
        base.Init(from, to, config);
        config.actionTypes = new ActionType[] { ActionType.DEAD };
    }

    public override IEnumerator ExeucteAction()
    {
        RegisterActionToBuff_ToOnly();
        broadCastEvent();
        if (config.flag1) { // flag1: dead == true
            if (RoleUtils.Instance.IsPrimaryEnemy(to))
            {
                // finish win
                Debug.Log("Game Win");
            }
            else if (RoleUtils.Instance.IsPlayer(to))
            {
                // finish lose
                Debug.Log("Game Lose");

            }
            else {
                RegisterActionToBuff_FromOnly();
                broadCastEvent();

            }
        }

        yield return null;
             
    }

    public override IEnumerator PostAction()
    {
        yield return null;
    }

    public override IEnumerator PreAction()
    {
        yield return null;
    }
}
