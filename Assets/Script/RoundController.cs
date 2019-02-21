using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour {
    public GameManager gameManager;
    public GameState gameState;

    public Role currentControl;

    public bool isPlayerRound;
    private bool isPlayerControlEnabled;

    GameObject whoIsControlling;
    GameObject roundText;


    private void Awake()
    {
        gameState = FindObjectOfType<GameState>();
        gameManager = FindObjectOfType<GameManager>();
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        whoIsControlling = canvas.transform.Find("WhoIsControl").gameObject;
        roundText = canvas.transform.Find("RoundText").gameObject;
    }

    private void Update()
    {
        
    }

    public Role SwitchControl() {
        if (IsAllRoleFinished()) {
            NextRound();
        }

        if (!IsPlayerSideFinished())
        {
            isPlayerRound = true;

            if (!gameState.player.roundFinished)
            {
                EnablePlayerControl();
                currentControl = gameState.player;
            }
            else
            {
                DisablePlayerControl();
                currentControl = GetNextPlaySideControl();
            }
        }
        else if (!IsEnemySideFinished()) {
            currentControl = GetNextEnemySideControl();
        }

        roundText.GetComponent<Text>().text = "Round " + GameState.GetRound();
        whoIsControlling.GetComponent<Text>().text = isPlayerRound ? "Play" : "Enemy";
        return currentControl;
    }

    public void NextRound()
    {
        gameState.AddRound();

        foreach (Role role in gameState.AllRoles())
        {
            role.roundFinished = false;
        }
    }

    private Role GetNextPlaySideControl() {
        foreach (Role role in gameState.GetPlayerSide()) {
            if (role.type != RoleType.Player && !role.roundFinished) {
                return role;
            }
        }

        return null;
    }

    private Role GetNextEnemySideControl()
    {
        if (!gameState.GetPrimaryEnemy().roundFinished)
        {
            return gameState.GetPrimaryEnemy();
        }
        else {
            foreach (Role role in gameState.GetEnemySide())
            {
                if (role.type != RoleType.PrimaryEnemy && !role.roundFinished)
                {
                    return role;
                }
            }

        }

        return null;
    }


    public bool IsAllRoleFinished() { return IsRoleGroupFinished(gameState.AllRoles()); }
    public bool IsPlayerSideFinished() { return IsRoleGroupFinished(gameState.GetPlayerSide()); }
    public bool IsEnemySideFinished() { return IsRoleGroupFinished(gameState.GetEnemySide()); }

    private bool IsRoleGroupFinished(List<Role> roles) {
        foreach (Role role in roles) {
            if (!role.roundFinished) {
                return false;
            }
        }
        return true;
    }

    // player controlling
    public bool IsPlayerControlling() { return currentControl.transform.gameObject.GetComponent<Player>() != null; }
    public void DisablePlayerControl() { isPlayerControlEnabled = false; }
    public void EnablePlayerControl() { isPlayerControlEnabled = true; }
    public bool IsPlayerControlEnabled() { return isPlayerControlEnabled; }
}
