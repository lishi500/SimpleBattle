using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtonClick : BaseButtonClick
{
    public override IEnumerator OnActionButtonClicked()
    {
        //if (roundController.IsPlayerControlEnabled())
        //{
        //    ActionConfig config = new ActionConfig().WithValue1(120f);

        //    Action action = ActionUtils.Instance.CreateAction("FireBallAction", gameState.player, gameState.enemy[0], config);
        //    StartCoroutine(action.Apply());


        //    PlayerRoundFinished();
        //}
        foreach (BaseBuff buff in gameState.player.GetBuffs()) {
            buff.Destory();
        }
        yield return null;
    }

}
