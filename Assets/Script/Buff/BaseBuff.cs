using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public  class BaseBuff : MonoBehaviour {
    public Image image;
    public string buffName;
    public int fromRoleId;
    public int roundLeft = -1;
    public int count;
    public BuffType type;
    public int order;
    public Role caster;
    public LoopEffect loopEffect;

    public float value1;
    public float value2;

    public float factor1;
    public float factor2;

    public bool isForever;
    public bool isDeBuff;

    public ActionType[] reactToType;
    public bool reactToFrom;
    public bool reactToAny = true;

    public delegate void BuffClearedDelegate();
    public delegate void BuffTriggedDelegate();
    public delegate void BuffDisperseDelegate();

    public enum BuffType
    {
        NONE,
        DOT,
        HOT,
        TRIGGER,
        ATTRIBUTE,
        ENHANCE,
        CONTROL,
        SLIENCE,
        THRON,
        TRANSFORM,
        IMMUNE,
        SHELL,
        SKILL
    }

    public abstract void Trigger();
    public abstract void RoundStartExecute();
    public abstract void RoundEndExecute();
    public abstract void OnBuffEvaluated(Role from, Role to, ActionConfig config);
    public abstract void PlayEffect();
    public abstract void OnBuffCleared();

    public virtual void OnBuffApplied() {}


    public void LateUpdate()
    {
        if (loopEffect != null) {
            if (loopEffect.playing)
            {
                loopEffect.playTime += Time.deltaTime;
                EffectUtils.Instance.loopEffectRePosition(loopEffect, GetRole().transform.position);
                if (loopEffect.playTime > loopEffect.effectTime)
                {
                    loopEffect.playing = false;
                    loopEffect.playTime = 0;
                }
            }
            else
            {
                loopEffect.waitingTime += Time.deltaTime;
                if (loopEffect.waitingTime > loopEffect.loopInterval)
                {
                    PlayEffect();

                    loopEffect.waitingTime = 0;
                    loopEffect.playing = true;
                }
            }
        }


    }



    public virtual void RegisterFromAction(Action action, Role from) {
        if (reactToFrom && IsFrom(from) && this.ReactToAction(action)) { 
            action.notifyAction += OnBuffEvaluated;
        }
    }

    public virtual void RegisterToAction(Action action, Role to)
    {
        if (!reactToFrom && IsTo(to) && this.ReactToAction(action))
        {
            action.notifyAction += OnBuffEvaluated;
        }
    }

    public void ReduceRound() {
        if (roundLeft != -1 && roundLeft > 0)
        {
            roundLeft--;
            if (roundLeft == 0) {
                Destory();
            }
        }
    }

    public virtual void ReduceCount() {
        count -= 1;
        if (count <= 0) {
            Destory();
        }
    }

    public virtual void SetCaster(Role caster)
    {
        this.caster = caster;
    }

    public bool ReactToAction(Action action) {
        if (reactToAny) {
            return ContainAnyType(action);
        } else {
            return ContainAllType(action);
        }
    }

    public bool ContainAllType(Action action) {
        return action.hasActionTypes(reactToType);
    }

    public bool ContainAnyType(Action action) {
        foreach (ActionType actionType in reactToType) {
            if (action.hasActionType(actionType)) {
                return true;
            }
        }

        return false;
    }

    public bool IsFrom(Role from) {
       return transform.parent.gameObject.GetInstanceID() == from.gameObject.GetInstanceID();
    }

    public bool IsTo(Role to) {
        return transform.parent.gameObject.GetInstanceID() == to.gameObject.GetInstanceID();
    }

    public Role GetRole() {
        return this.transform.parent.gameObject.GetComponent<Role>();
    }


    public virtual void Destory() {

        BuffClearedDelegate buffCleared = new BuffClearedDelegate(OnBuffCleared);
        buffCleared();
        if (this != null) {
            Destroy(this.gameObject);
        }
    }
}
