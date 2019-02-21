using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : Skill {


    public override IEnumerator PostSkill()
    {
        yield return null;
    }

    public override IEnumerator PreSkill()
    {
        yield return null;
    }

    public override IEnumerator SkillAction()
    {
        ActionConfig actionConfig = new ActionConfig();
        actionConfig.damage = from.attack;
        Action damageAction = ActionUtils.Instance.CreateAction("AttackAction", from, to, actionConfig);

        yield return StartCoroutine(damageAction.Apply());
    }


}
