using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDebuff : BaseBuff {

    private void Awake()
    {
        isDeBuff = true;
        reactToType = new ActionType[] { ActionType.DISPEL };
        reactToFrom = true;
        buffName = "Dot";
    }

    public override void SetCaster(Role caster)
    {
        this.caster = caster;
        caster.notifyRoleFirstActionEnd += RoundStartExecute;
    }

    public override void OnBuffEvaluated(Role from, Role to, ActionConfig config)
    {
        
    }

    public override void Trigger()
    {
    }

    public override void RoundStartExecute()
    {
        ActionConfig config = new ActionConfig().WithValue1(value1);
        Action action = ActionUtils.Instance.CreateAction("DotAction", caster, GetRole(), config);
        StartCoroutine(action.Apply());
 
    }

    public override void RoundEndExecute()
    {

    }
}
