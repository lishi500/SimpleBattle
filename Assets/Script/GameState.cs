using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public Player player = null;
    public Enemy[] enemy = null;

    public LinkedList<Action> usedAction = new LinkedList<Action>();
    private int maxActionSize = 8;

    private static int round = 0;


    private void Awake()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectsOfType<Enemy>();
    }

    public static int GetRound()
    {
        return round;
    }

    public void SaveState() {

    }

    public void AddRound()
    {
        round++;
    }

    public void AddUsedAction(Action action)
    {
        // TODO
        if (usedAction.Count >= maxActionSize)
        {
            usedAction.RemoveFirst();
        }
        usedAction.AddLast(action);
    }



    public Action GetLastAction()
    {
        return usedAction.Last.Value;
    }

    public List<Role> AllRoles()
    {
        List<Role> all = new List<Role>();
        all.Add(player);
        all.AddRange(enemy);

        return all;
    }

    public List<Role> GetPlayerSide() {
        List<Role> playerSide = new List<Role>();
        playerSide.Add(player);

        return playerSide;
    }

    public List<Role> GetEnemySide()
    {
        List<Role> enemySide = new List<Role>();
        enemySide.AddRange(enemy);

        return enemySide;
    }

    public Role GetPrimaryEnemy() {
        foreach (Role role in GetEnemySide()) {
            if (role.type == RoleType.PrimaryEnemy) {
                return role;
            }
        }
        return null;
    }
}
   
   
