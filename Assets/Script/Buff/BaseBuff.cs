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

    public void Awake()
    {
        
    }

    public virtual void RegisterFromAction(Action action, Role from) {
        if (reactToFrom && isFrom(from) && this.reactToAction(action)) { 
            action.notifyAction += OnBuffEvaluated;
        }
    }

    public virtual void RegisterToAction(Action action, Role to)
    {
        if (!reactToFrom && isTo(to) && this.reactToAction(action))
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

    public bool reactToAction(Action action) {
        if (reactToAny) {
            return containAnyType(action);
        } else {
            return containAllType(action);
        }
    }

    public bool containAllType(Action action) {
        return action.hasActionTypes(reactToType);
    }

    public bool containAnyType(Action action) {
        foreach (ActionType type in reactToType) {
            if (action.hasActionType(type)) {
                return true;
            }
        }

        return false;
    }

    public bool isFrom(Role from) {
       return transform.parent.gameObject.GetInstanceID() == from.gameObject.GetInstanceID();
    }

    public bool isTo(Role to) {
        return transform.parent.gameObject.GetInstanceID() == to.gameObject.GetInstanceID();
    }

    public Role GetRole() {
        return this.transform.parent.gameObject.GetComponent<Role>();
    }

    public void OnBuffCleared() { }

    public virtual void Destory() {

        BuffClearedDelegate buffCleared = new BuffClearedDelegate(OnBuffCleared);
        buffCleared();
        if (this != null) {
            Destroy(this.gameObject);
        }
    }
}
