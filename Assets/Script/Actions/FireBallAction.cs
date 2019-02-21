using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAction : Action {
    private CurveMove curveMove;

    public override void Init(Role from, Role to, ActionConfig config)
    {
        base.Init(from, to, config);
        config.actionTypes = new ActionType[] { ActionType.DAMAGE, ActionType.MIGICAL, ActionType.AOE };
    }


    public override IEnumerator ExeucteAction()
    {
        RegisterActionToBuff();

        if (ActionUtils.Instance.ShouldTakeDamage(to, config.value1, ActionType.MIGICAL)) {
            broadCastEvent();
            CastFireBallAnimation();
            yield return new WaitForSeconds(1f);
            to.ReduceHealth(config.value1);
            EffectUtils.Instance.DamageEffect(to.transform.position, config.value1, ActionType.MIGICAL);
            ApplyDebuff();
        }

        yield return null;
    }

    private void ApplyDebuff()
    {
        GameObject dotDebuff = PrafabUtils.Instance.create("dotDebuff");
        to.AddBuff(dotDebuff, from);
    }

    public void OnFireBallMoveEnd(CurveMove.CurveMoveStatus status) {
        if (status == CurveMove.CurveMoveStatus.END) {
            Debug.Log("Catch fireball movement end event");
            CastFireBallExplosion();

        }
    }

    public CurveMove GetAnimationMovement() {
        return curveMove;
    }
    private void CastFireBallExplosion() {
        GameObject fireBallExplosion = PrafabUtils.Instance.create("fireBallExplosion", to.transform.position, null);
    }

    private void CastFireBallAnimation() {
        GameObject fireBall = PrafabUtils.Instance.create("fireBall", from.transform.position, null);
        fireBall.AddComponent<CurveMove>();
        curveMove = fireBall.GetComponent<CurveMove>();

        curveMove.SetPosition(from.transform.position, to.transform.position, 125, 1f);
        curveMove.Animate();

        curveMove.notifyCurveMoveEvent += OnFireBallMoveEnd;
        //PlayParticle()
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
