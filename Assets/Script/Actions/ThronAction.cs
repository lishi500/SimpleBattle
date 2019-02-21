using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThronAction : Action {
   
    public override void Init(Role from, Role to, ActionConfig config)
    {
        base.Init(from, to, config);
        config.actionTypes = new ActionType[] { ActionType.THRON, ActionType.TRUE_DAMAGE };
    }



    public override IEnumerator ExeucteAction()
    {
        RegisterActionToBuff();
        if (ActionUtils.Instance.ShouldTakeDamage(to, config.damage, ActionType.TRUE_DAMAGE)) {
            broadCastEvent();

            to.ReduceHealth(config.value1);
            EffectUtils.Instance.DamageEffect(to.transform.position, config.value1, ActionType.TRUE_DAMAGE);
            yield return 1;
            EffectUtils.Instance.BloodDrippingEffect(to.transform.position, to.gameObject);
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
