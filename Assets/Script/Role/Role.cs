using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Role : MonoBehaviour
{

    string roleName;
    int id;

    public int attack;
    public int defence;
    public bool isAlive;
    public bool fristActionInRound = true;
    public bool roundFinished = false;
    public List<RoleStatus> status;
    public RoleType type;
    [SerializeField] public List<GameObject> buffs;

    public Attribute attribute;
    [HideInInspector]
    public HealthBar healthBar;
    private GameObject healthBarObject;

    [HideInInspector]
    public List<Action> actionsInRound;

    private GameManager gameManager;
    private GameState gameState;
    public BasicAI AIController;


    public delegate void RoleFirstActionEndDelegate();
    public event RoleFirstActionEndDelegate notifyRoleFirstActionEnd;

    public virtual void Awake()
    {
        if (attribute == null)
        {
            attribute = new Attribute();
        }
        healthBarObject = transform.Find("HealthBar").gameObject;
        healthBar = healthBarObject.GetComponent<HealthBar>();
        healthBar.InitialHealth(attribute.HP, attribute.maxHP, attribute.shield);

        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameController.GetComponent<GameManager>();
        gameState = gameController.GetComponent<GameState>();
        actionsInRound = new List<Action>();
        status = new List<RoleStatus>();
        //healthBar = FindObjectOfType<HealthBar>();
        //healthBar.SetHealth(50, 100);
    }

    public void LateUpdate()
    {

    }

    public abstract void NextAction();


    public List<BaseBuff> GetBuffs()
    {
        List<BaseBuff> baseBuffs = new List<BaseBuff>();
        foreach (GameObject buffObj in buffs)
        {
            if (buffObj != null)
            {
                BaseBuff buff = buffObj.GetComponent<BaseBuff>();
                if (buff != null)
                {
                    baseBuffs.Add(buff);
                }
            }
        }

        return baseBuffs;
    }


    public void AddBuff(GameObject buffPrafab, Role caster)
    {
        bool exist = false;
        BaseBuff buff = buffPrafab.GetComponent<BaseBuff>();
        Vector3 scale = buffPrafab.transform.localScale;
        buff.fromRoleId = caster.GetInstanceID();
        buff.SetCaster(caster);
        foreach (BaseBuff baseBuff in GetBuffs())
        {
            if (baseBuff.buffName == buff.buffName && baseBuff.fromRoleId == caster.GetInstanceID())
            {
                buff.order = baseBuff.order;
                exist = true;
                ReplaceBuff(baseBuff, buffPrafab);
            }
        }

        if (!exist)
        {
            buffPrafab.transform.SetParent(this.transform);
            buffPrafab.transform.position = this.transform.position;
            buff.order = buffs.Count;
            buffs.Add(buffPrafab);
        }
        correctBuffPosition(buffPrafab);
        buffPrafab.transform.localScale = scale;
    }

    public void RoleActionEnd(Action action)
    {
        actionsInRound.Add(action);

        if (fristActionInRound)
        {
            fristActionInRound = false;
            if (notifyRoleFirstActionEnd != null)
            {
                notifyRoleFirstActionEnd();
            }
        }
    }

    public void RoleControlEnd()
    {
        actionsInRound.Clear();
        roundFinished = true;
        gameManager.RoleControlFinished(this);
    }

    public void RoleControlStart()
    {
        roundFinished = false;
        fristActionInRound = true;
    }

    private void correctBuffPosition(GameObject buffPrafab)
    {
        Vector3 healthBarPos = healthBarObject.transform.position;
        healthBarPos.y -= 1.25f;
        healthBarPos.x -= 1.25f + buffPrafab.GetComponent<BaseBuff>().order * 0.6f;
        buffPrafab.transform.position = healthBarPos;
    }

    private void ReplaceBuff(BaseBuff baseBuff, GameObject buffPrafab)
    {
        GameObject oldBuff = baseBuff.transform.gameObject;
        buffPrafab.transform.position = oldBuff.transform.position;
        buffPrafab.transform.SetParent(oldBuff.transform.parent);
        buffs[baseBuff.order] = buffPrafab;
        Destroy(oldBuff);
    }

    public float GetAttributeValue(AttributeType type)
    {
        return RoleUtils.Instance.GetAttributeValueByType(this, type);
    }


    public float ReduceHealth(float amount)
    {
        if (attribute.shield > 0)
        {
            float amountLeft = Mathf.Max(amount - attribute.shield, 0);
            ReduceShield(amount);

            amount = amountLeft;
        }

        attribute.HP -= amount;
        if (attribute.HP <= 0)
        {
            attribute.HP = 0;
            SetAlive(false);
        }

        return attribute.HP;
    }

    public float AddHealth(float amount)
    {
        if (isAlive)
        {
            attribute.HP = Mathf.Min(attribute.HP + amount, attribute.maxHP);
        }

        return attribute.HP;
    }

    public float ReduceShield(float amount)
    {
        attribute.shield -= amount;
        if (attribute.shield <= 0)
        {
            attribute.shield = 0;
        }

        return attribute.shield;
    }

    public void SetAlive(bool alive)
    {
        this.isAlive = alive;
        if (!isAlive)
        {

            Action lastAction = gameState.GetLastAction();
            Role murder = lastAction == null ? this : lastAction.from;
            ActionConfig actionConfig = new ActionConfig().WithFlag1(true);
            Action action = ActionUtils.Instance.CreateAction("DeadAction", murder, this, actionConfig);

            StartCoroutine(action.Apply());
        }
    }

    public int getId()
    {
        return this.GetInstanceID();
    }

    public void UpdateHealth()
    {
        healthBar.SetHealth(attribute.HP, attribute.maxHP, attribute.shield);
    }

    public void SetAi(BasicAI ai)
    {
        this.AIController = ai;
    }

    public bool CanControl()
    {
        return !RoleUtils.Instance.HasAnyStatus(status, new RoleStatus[] { RoleStatus.SLEEP, RoleStatus.STONE, RoleStatus.STUN});
    }

    public bool CanCastSkill()
    {
        return !RoleUtils.Instance.HasAnyStatus(status, new RoleStatus[] { RoleStatus.SCLIENCE });

    }

    public bool CanBeHeal()
    {
        return isAlive && !RoleUtils.Instance.HasAnyStatus(status, (new RoleStatus[] { RoleStatus.IMMU }));
    }



    public void AddStatue(RoleStatus roleStatus)
    {
        if (!status.Contains(roleStatus))
        {
            status.Add(roleStatus);
        }
    }

    public void RemoveStatus(RoleStatus roleStatus) {
        if (status.Contains(roleStatus))
        {
            status.Remove(roleStatus);
        }

    }


}
