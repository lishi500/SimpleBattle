using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamageBuff : BaseBuff {

    private void Awake()
    {
        reactToType = new ActionType[] { ActionType.DAMAGE };
        reactToFrom = true;
        buffName = "DoubleDamage";
    }

    public override void OnBuffEvaluated(Role from, Role to, ActionConfig config)
    {
        Debug.Log("damageAction OnCalculateDamage");
        config.value1 = Mathf.Max(config.value1 * 2, config.damage * 2);
        ReduceCount();
    }


    public override void Trigger()
    {
    }

    public override void RoundStartExecute()
    {
    }

    public override void RoundEndExecute()
    {
    }

    public override void PlayEffect()
    {
    }

    public override void OnBuffCleared()
    {
    }
}
