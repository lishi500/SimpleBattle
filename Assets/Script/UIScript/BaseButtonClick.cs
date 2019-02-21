using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseButtonClick : MonoBehaviour {
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public GameState gameState;
    [HideInInspector] public RoundController roundController;

    public bool isEnable;

    public abstract IEnumerator OnActionButtonClicked();

    private void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameController.GetComponent<GameManager>();
        gameState = gameController.GetComponent<GameState>();
        roundController = gameController.GetComponent<RoundController>();

    }
    public void ButtonClicked() {
        StartCoroutine(OnActionButtonClicked());
    }

    public void PlayerRoundFinished() {
        gameState.player.RoleControlEnd();
    }

    public void SubscribeSkillEvent(Skill skill) {
        skill.onSkillEvent += OnSkillEvent;
    }

    public virtual void OnSkillEvent(SkillStatus status, Skill skill) {
        if (status == SkillStatus.EndSkill) {
            Debug.Log("Skill end event ");
            PlayerRoundFinished();
        }
    }

}
