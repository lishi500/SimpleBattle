using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAction : Action
{
    public override IEnumerator ExeucteAction()
    {
        if (hasActionType(ActionType.DAMAGE))
        {
            yield return StartCoroutine(DamageAction());
        }
        else if (hasActionType(ActionType.HEAL)) {
            yield return StartCoroutine(HealAction());
        }
        else if (hasActionType(ActionType.SHELL))
        {
            yield return StartCoroutine(HealAction());
        }
        else if (hasActionType(ActionType.AOE))
        {

        }
        else if (hasActionType(ActionType.CONTROL))
        {
            yield return StartCoroutine(ControlAction());
        }

        yield return null;
    }

    private IEnumerator ControlAction()
    {
        RegisterActionToBuff();
        broadCastEvent();

        switch (config.controlActionType) {
            case ControlActionType.STUN:
        }        

        yield return null;
    }

    private IEnumerator DamageAction() {
        RegisterActionToBuff();
        if (ActionUtils.Instance.ShouldTakeDamage(to, config.value1, GetDamageType()))
        {
            broadCastEvent();

            to.ReduceHealth(config.actionValue);

            EffectUtils.Instance.DamageEffect(to.transform.position, config.actionValue, GetDamageType());
            yield return 1;
            EffectUtils.Instance.BloodDrippingEffect(to.transform.position, to.gameObject);
        }
        yield return null;
    }

    private IEnumerator HealAction() {
        RegisterActionToBuff();
        if (to.CanBeHeal()) {
            broadCastEvent();

            to.AddHealth(config.actionValue);
            EffectUtils.Instance.HealEffect(to.transform.position, config.actionValue);
        }

        yield return null;
    }

    private IEnumerator ShieldAction()
    {
        RegisterActionToBuff();
        if (to.CanBeHeal())
        {
            broadCastEvent();

            to.AddHealth(config.actionValue);
            EffectUtils.Instance.HealEffect(to.transform.position, config.actionValue);
        }

        yield return null;
    }

    public override IEnumerator PostAction()
    {
        yield return null;
    }

    public override IEnumerator PreAction()
    {
        if (config.isApplyActionBeforeEffect) {
            StartCoroutine(PreActionEffect());
        } else {
            yield return StartCoroutine(PreActionEffect());
        }
    }


    private IEnumerator PreActionEffect() {
        if (config.effectName != null)
        {
            UtilHelper.LogTime(this.GetInstanceID() + " wait for: " + config.effectStartTime + " > ");

            yield return new WaitForSeconds(config.effectStartTime);

            GameObject effectPrafab = PrafabUtils.Instance.create(config.effectName);
            if (effectPrafab != null)
            {
                effectPrafab.transform.position = GetEffectPosition(config.startPosition);
                if (config.isThrowCurve)
                {
                    UtilHelper.LogTime(this.GetInstanceID() + " start: " );

                    StartCurveAnimation(effectPrafab);
                }
                else {
                    EffectUtils.Instance.PlayParticle(effectPrafab);
                }


                yield return new WaitForSeconds(config.effectDuration);
                UtilHelper.LogTime(this.GetInstanceID() + " finished: ");

                effectPrafab.transform.position = GetEffectPosition(config.endPosition);
            }
        }
        yield return null;
    }

    private void StartCurveAnimation(GameObject effectObj) {
        effectObj.AddComponent<CurveMove>();
        CurveMove curveMove = effectObj.GetComponent<CurveMove>();
        config.curveHeight = new System.Random().Next(-249, 249);

        curveMove.SetPosition(GetEffectPosition(config.startPosition), GetEffectPosition(config.endPosition), config.curveHeight, config.curveDuration == 0 ? 1f : config.curveDuration);
        curveMove.Animate();

        //curveMove.notifyCurveMoveEvent += OnFireBallMoveEnd;

    }

    private Vector3 GetEffectPosition(EffectPositionType positionType) {
        Vector3 position = new Vector3();
        switch (positionType) {
            case EffectPositionType.SOURCE_POSITION:
                position = from.transform.position;
                break;
            case EffectPositionType.TARGET_POSITION:
                position = to.transform.position;
                break;
            case EffectPositionType.CENTOR:

                break;

            default:

                position = from.transform.position;
                break;

        }

        return position;
    }

    private void LogTime(string message) {
        Debug.Log(string.Format("{0:HH:mm:ss tt}", DateTime.Now) + ": " + message);
    }
}
