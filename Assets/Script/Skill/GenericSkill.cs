using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSkill : Skill
{
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
        SkillActionConfig[] actions = config.skillActionConfigs;
        foreach (SkillActionConfig config in actions) {
            StartCoroutine(StartActionAsync(config));
        }

        yield return null;
    }

    public IEnumerator StartActionAsync(SkillActionConfig config) {
        yield return null;
    }

    public Action ConvertToAction(SkillActionConfig config) {
        return null;
    }

}
