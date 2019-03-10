using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBuff : BaseBuff {

    private void Awake()
    {
        reactToType = new ActionType[] { ActionType.DAMAGE };
        reactToFrom = false;
        buffName = "Shell";
        value1 = 100;
    }

    public override void OnBuffEvaluated(Role from, Role to, ActionConfig config)
    {
        if (value1 >= config.value1)
        {
            value1 = value1 - config.value1;
            config.value1 = 0;
        }
        else {
            config.value1 = config.value1 - value1;
            value1 = 0;
            //Destory();
        }
    }

    public override void Trigger()
    {
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void RoundStartExecute()
    {
    }

    public override void RoundEndExecute()
    {
    }

    public override void PlayEffect()
    {
    }

    public override void OnBuffCleared()
    {
    }
}
