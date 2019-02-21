using System;

[Serializable]
public class ActionConfig {
    public float damage;
    public float heal;
    public float duration;
    public float times;
    public float factor;
    public bool flag;
    public bool displayMessage;


    public float value1;
    public float value2;
    public float value3;
    public bool flag1;
    public bool flag2;
    public string message;

    // ------------------ new
    public float actionValue;
    public ActionType damageType;
    public ActionType[] actionTypes;

    // effect 
    public string effectName;
    public float effectStartTime;
    public float effectDuration;
    public EffectPositionType startPosition;
    public EffectPositionType endPosition;
    // curve
    public bool isThrowCurve;
    public float curveHeight;
    public float curveDuration;


    // ----- flag
    public bool isApplyActionBeforeEffect;
    public bool isControl;
    public bool isActionDenied;


    // ------ control
    public ControlActionType controlActionType;


    public ActionConfig WithValue1(float value) {
        this.value1 = value;
        return this;
    }
    public ActionConfig WithValue2(float value)
    {
        this.value2 = value;
        return this;
    }
    public ActionConfig WithValue3(float value)
    {
        this.value3 = value;
        return this;
    }
    public ActionConfig WithFlag1(bool flag)
    {
        this.flag1 = flag;
        return this;
    }
    public ActionConfig WithFlag2(bool flag)
    {
        this.flag2 = flag;
        return this;
    }
    public ActionConfig WithMessage(string message)
    {
        this.message = message;
        return this;
    }
}
