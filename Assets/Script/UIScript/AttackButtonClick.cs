using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButtonClick : BaseButtonClick {
    Player player;
    Enemy enemy;
     
    public override IEnumerator OnActionButtonClicked()
    {
        if (roundController.IsPlayerControlEnabled())
        {
            player = gameState.player;
            enemy = gameState.enemy[0];

            //ActionConfig actionConfig = new ActionConfig();
            //actionConfig.damage = player.attack;
            //Action damageAction =  ActionUtils.Instance.CreateAction("AttackAction", player, enemy, actionConfig);


            /* Skill attack = SkillUtils.Instance.CreateSkill("AttackSkill", player, enemy, null);
             SubscribeSkillEvent(attack);

             yield return StartCoroutine(attack.CastSkill());
             PlayerRoundFinished();
             */
            //System.Random rnd = new System.Random();

            //for (int i = 0; i < 2; i++)
            //{
            //    ActionConfig actionConfig = ActionUtils.Instance.LoadAction("GenericAction");

            //    actionConfig.effectStartTime = (float)rnd.NextDouble() * 10;
            //    Action action = ActionUtils.Instance.CreateAction("GenericAction", gameState.player, gameState.enemy[0], actionConfig);
            //    StartCoroutine(action.Apply());
            //}
            StartCoroutine(ExecuteHolder());
        }

        yield return null;
    }

    private IEnumerator ExecuteHolder() {
        GameObject actionSkillHolder = GameObject.Find("ActionSkillHolder");
        Action action = actionSkillHolder.GetComponent<Action>();
        action.SetRoles(gameState.player, gameState.enemy[0]);

        yield return StartCoroutine(action.Apply());
    }
}
