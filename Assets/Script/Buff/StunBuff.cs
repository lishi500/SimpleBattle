using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBuff : BaseBuff
{
    public override void OnBuffEvaluated(Role from, Role to, ActionConfig config)
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

    public override void OnBuffApplied()
    {
        base.OnBuffApplied();
    }

    public override void PlayEffect()
    {
        EffectUtils.Instance.createEffectByLoopEffect(loopEffect, GetRole().gameObject.transform.position);
    }

    public override void OnBuffCleared()
    {
        List<BaseBuff> buffs = GetRole().GetBuffs();
        foreach (BaseBuff buff in buffs) {
            Debug.Log(buff.GetType());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        loopEffect =  EffectUtils.Instance.initLoopEffect("StunEffect", 2.5f, 5f, 0, 200f);
        RoleUtils.Instance.AddControlStatus(GetRole(), ControlActionType.STUN);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
}
