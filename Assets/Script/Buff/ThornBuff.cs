using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornBuff : BaseBuff {

    public ThornBuff() {
        reactToType = new ActionType[] { ActionType.DAMAGE, ActionType.PHYSICAL };
        reactToFrom = false;
        reactToAny = false;
        buffName = "ThornBuff";
    }

    public override void OnBuffCleared()
    {
    }

    public override void OnBuffEvaluated(Role from, Role to, ActionConfig config)
    {
        if (ActionUtils.Instance.ShouldTakeDamage(from, config.value1, ActionType.TRUE_DAMAGE)) {
            ActionConfig actionConfig = new ActionConfig().WithValue1(config.value1 * value1);
            Action action = ActionUtils.Instance.CreateAction("ThronAction", to, from, actionConfig);
            StartCoroutine(action.Apply());

        }
    }

    public override void PlayEffect()
    {
    }

    public override void RoundEndExecute()
    {
    }

    public override void RoundStartExecute()
    {
    }

    public override void Trigger()
    {
    }

}
