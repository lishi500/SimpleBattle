using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackAction : Action {
   
   
    public override void Init(Role from, Role to, ActionConfig config) {
        base.Init(from, to, config);
        config.actionTypes = new ActionType[] { ActionType.DAMAGE, ActionType.PHYSICAL };
    }


    public override IEnumerator ExeucteAction()
    {
        RegisterActionToBuff();
        
        config.value1 = config.damage;
        if (ActionUtils.Instance.ShouldTakeDamage(to, config.value1, ActionType.PHYSICAL)) {
            broadCastEvent();

            float actualDamage = config.value1;

            to.ReduceHealth(actualDamage);

            EffectUtils.Instance.DamageEffect(to.transform.position, actualDamage, ActionType.PHYSICAL);
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
