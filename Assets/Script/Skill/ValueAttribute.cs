using System;

[Serializable]
public class ValueAttribute {
    public AttributeType attributeType;

    public float baseValue;
    public float attributeFactor; // + attributeType.getValue * attributeFactor

    public float roundFactor; // increase or decrease every round
}
