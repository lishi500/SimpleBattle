using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotAction : Action {
    ActionType damageType;

    public override void Init(Role from, Role to, ActionConfig config)
    {
        base.Init(from, to, config);
        config.actionTypes = new ActionType[] { ActionType.DOT, ActionType.PHYSICAL };
        this.damageType = ActionType.PHYSICAL;
    }

    public void SetDamageType(ActionType damageType) {
        this.damageType = damageType;
        config.actionTypes[1] = damageType;
    }

    public override IEnumerator ExeucteAction()
    {
        RegisterActionToBuff();
        if (ActionUtils.Instance.ShouldTakeDamage(to, config.value1, damageType)) {
            broadCastEvent();

            to.ReduceHealth(config.value1);
            EffectUtils.Instance.DamageEffect(to.transform.position, config.value1, damageType);
            EffectUtils.Instance.CastAutoPlayEffect(to.transform.position, "fireDotEffect");
        }

        yield return null;
    }

    public override IEnumerator PreAction()
    {
        yield return null;
    }

    public override IEnumerator PostAction()
    {
        yield return null;
    }
}
