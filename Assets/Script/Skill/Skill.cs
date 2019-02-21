using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Skill : MonoBehaviour {

    [HideInInspector] GameManager gameManager;
    [HideInInspector] GameState gameState;

    public SkillConfig config;
    public Role from;
    public Role to;
    [HideInInspector] public SkillStatus status = SkillStatus.PreSkill;

    public delegate void SkillDelegate(SkillStatus status, Skill skill);
    public event SkillDelegate onSkillEvent;

    public void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameController.GetComponent<GameManager>();
        gameState = gameController.GetComponent<GameState>();
    }

    public void Init(Role from, Role to, SkillConfig config) {
        this.from = from;
        this.to = to;
        this.config = config;
    }

    public IEnumerator CastSkill() {
        BroadCastSkillStateEvent(SkillStatus.PreSkill);
        yield return StartCoroutine(PreSkill());
        BroadCastSkillStateEvent(SkillStatus.PreSkillEnd);


        BroadCastSkillStateEvent(SkillStatus.StartAction);
        yield return StartCoroutine(SkillAction());
        BroadCastSkillStateEvent(SkillStatus.StartActionEnd);


        BroadCastSkillStateEvent(SkillStatus.PostSkill);
        yield return StartCoroutine(PostSkill());
        BroadCastSkillStateEvent(SkillStatus.EndSkill);

        EndSkill();
        yield return null;

    }

    public abstract IEnumerator PreSkill();
    public abstract IEnumerator SkillAction();
    public abstract IEnumerator PostSkill();

    public void EndSkill() {
        Destroy(this.gameObject);
    }

    private void BroadCastSkillStateEvent(SkillStatus status) {
        this.status = status;
        if (onSkillEvent != null)
        {
            onSkillEvent(status, this);
        }
    }


}
