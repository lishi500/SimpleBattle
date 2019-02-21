using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
abstract public class Action : MonoBehaviour
{
    public GameManager gameManager;
    public GameState gameState;

    public string actionName;
    public Role from;
    public Role to;
    [SerializeField] public ActionConfig config;
    public float actionStartTime;

    public delegate void ActionDelegate(Role from, Role to, ActionConfig config);
    public event ActionDelegate notifyAction;

    public delegate void ActionPreDelegate(Role from, Role to, ActionConfig config);
    public event ActionPreDelegate onPreAction;
    public delegate void ActionStartDelegate(Role from, Role to, ActionConfig config);
    public event ActionStartDelegate oActionnStart;
    public delegate void ActionPostDelegate(Role from, Role to, ActionConfig config);
    public event ActionPostDelegate onPostAction;
    public delegate bool ActionEndDelegate(Role from, Role to, ActionConfig config);
    public event ActionEndDelegate onActionEnd;


    public void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameController.GetComponent<GameManager>();
        gameState = gameController.GetComponent<GameState>();
        gameState.AddUsedAction(this);
    }

    public virtual void Init(Role from, Role to, ActionConfig config)
    {
        this.from = from;
        this.to = to;
        this.config = config;
    }

    public void SetRoles(Role from, Role to)
    {
        this.from = from;
        this.to = to;
    }

    public IEnumerator Apply()
    {
        if (onPreAction != null)
        {
            onPreAction(from, to, config);
        }
        yield return StartCoroutine(PreAction());

        if (oActionnStart != null)
        {
            oActionnStart(from, to, config);
        }
        yield return StartCoroutine(ExeucteAction());

        if (onPostAction != null)
        {
            onPostAction(from, to, config);
        }
        yield return StartCoroutine(PostAction());

        if (onActionEnd != null)
        {
            onActionEnd(from, to, config);
        }
        Destroy();
    }

    public abstract IEnumerator PreAction();
    public abstract IEnumerator ExeucteAction();
    public abstract IEnumerator PostAction();


    public void broadCastEvent()
    {
        if (notifyAction != null)
        {
            notifyAction(from, to, config);
        }
    }

    public virtual void RegisterActionToBuff()
    {
        RegisterActionToBuff_FromOnly();
        RegisterActionToBuff_ToOnly();
    }

    public virtual void RegisterActionToBuff_FromOnly()
    {
        List<BaseBuff> fromBuffs = from.GetBuffs();

        if (fromBuffs != null)
        {
            foreach (BaseBuff buff in fromBuffs)
            {
                buff.RegisterFromAction(this, from);
            }
        }
    }

    public virtual void RegisterActionToBuff_ToOnly()
    {
        List<BaseBuff> toBuffs = to.GetBuffs();

        if (toBuffs != null)
        {
            foreach (BaseBuff buff in toBuffs)
            {
                buff.RegisterToAction(this, to);
            }
        }
    }

    public bool hasActionType(ActionType type)
    {
        return Array.IndexOf(config.actionTypes, type) > -1;
    }

    public void ActionEnd()
    {
        if (from != null)
        {
            from.RoleActionEnd(this);
        }
    }

    public bool hasActionTypes(ActionType[] types)
    {
        foreach (ActionType type in types)
        {
            if (!hasActionType(type))
            {
                return false;
            }
        }

        return true;
    }

    public Action LoadAction(ActionData data)
    {
        this.actionName = data.actionName;
        this.config = data.config;
        this.config.actionTypes = data.actionTypes;
        this.actionStartTime = data.actionStartTime;

        return this;
    }

    public void Destroy()
    {
        UtilHelper.LogTime(this.GetInstanceID() + " distory: ");

        //Destroy(this.gameObject);
    }

    public ActionType GetDamageType()
    {
        if (hasActionType(ActionType.TRUE_DAMAGE))
        {
            return ActionType.TRUE_DAMAGE;
        }
        else if (hasActionType(ActionType.MIGICAL))
        {
            return ActionType.MIGICAL;
        }
        else if (hasActionType(ActionType.PHYSICAL))
        {
            return ActionType.PHYSICAL;
        }

        return ActionType.NONE;
    }
}