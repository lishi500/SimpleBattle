using System;

[Serializable]
public class SkillActionConfig {
    public string actionName;
    public float actionStartTime;
    public ActionType damageType;
    public ValueAttribute valueAttribute;

    public SkillActionConfig() { }

    public SkillActionConfig(string actionName, float actionStartTime, ActionType damageType, ValueAttribute valueAttribute)
    {
        this.actionName = actionName;
        this.actionStartTime = actionStartTime;
        this.damageType = damageType;
        this.valueAttribute = valueAttribute;
    }
}
