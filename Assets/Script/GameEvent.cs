using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour {

    public delegate void RoundEndDelegate(int round);
    public event RoundEndDelegate notifyRoundEnd;

    public void RoundEnd(int round) {
        if (notifyRoundEnd != null) {
            notifyRoundEnd(round);
        }
    }

    public delegate void RoundStartDelegate(int round);
    public event RoundStartDelegate notifyRoundStart;

    public void RoundStart(int round)
    {
        if (notifyRoundStart != null)
        {
            notifyRoundStart(round);
        }
    }

    public delegate void ControllSideChangeDelegate(bool isPlayer);
    public event ControllSideChangeDelegate notifyControllSideChange;

    public void ControllSideChange(bool isPlayer)
    {
        if (notifyControllSideChange != null)
        {
            notifyControllSideChange(isPlayer);
        }
    }

    public delegate void ControllChangeDelegate(Role role);
    public event ControllChangeDelegate notifyControllChange;

    public void ControllChange(Role role)
    {
        if (notifyControllChange != null)
        {
            notifyControllChange(role);
        }
    }
}
