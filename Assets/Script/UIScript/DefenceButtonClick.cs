using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceButtonClick : BaseButtonClick {
    Player player;
    public GameObject saveObj;

    public override IEnumerator OnActionButtonClicked()
    {
        //if (roundController.IsPlayerControlEnabled())
        //{
        //    Debug.Log("Add buff");
        //    player = gameState.player;
        //    Object ss = Resources.Load("Prafabs/DoubleDamageBuff");
        //    GameObject doubleDamageBuff = (GameObject)Object.Instantiate(Resources.Load("Prafabs/DoubleDamageBuff"));
        //    player.AddBuff(doubleDamageBuff, player);

        //    PlayerRoundFinished();
        //}
        if (saveObj != null) {
            Skill skill = saveObj.GetComponent<Skill>();
            if (skill != null ) {
                SkillUtils.Instance.SaveSillConfigToJson(skill);
            }

            Action action = saveObj.GetComponent<Action>();
            if (action != null)
            {
                ActionUtils.Instance.SaveActionConfigToJson(action);
            }
        }

        yield return null;
    } 

}
