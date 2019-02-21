using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoleUtils : MonoBehaviour {
    GameState gameState;

   
    private static RoleUtils _instance;
    public static RoleUtils Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameState = gameController.GetComponent<GameState>();
    }

    public bool IsPlayer(Role role) {
        return role.GetComponent<Player>() != null && role.transform.tag == "Player"; 
    }

    public bool IsEnemy(Role role) {
        return role.GetComponent<Enemy>() != null && role.transform.tag == "Enemy";
    }

    public bool IsPrimaryEnemy(Role role) {
        return role.GetComponent<Enemy>() != null && role.transform.tag == "Enemy" && role.GetComponent<Enemy>().type == RoleType.PrimaryEnemy;

    }

    public float GetAttributeValueByType(Role role, AttributeType type) {
        Attribute attribute = role.attribute;

        switch (type) {
            case AttributeType.ATTACK:
                return attribute.attack;
            case AttributeType.MAIGC:
                return attribute.magic;
            case AttributeType.LEVEL:
                return attribute.level;
            case AttributeType.MAX_HP:
                return attribute.maxHP;
            case AttributeType.HP:
                return attribute.HP;
            case AttributeType.SHELL:
                return attribute.shield;
            case AttributeType.ROUND:
                return GameState.GetRound();
            case AttributeType.LOTT:
                return (float) new System.Random().NextDouble();
            case AttributeType.NUM_ALLIS:
                if (IsEnemy(role))
                {
                    return gameState.GetEnemySide().Count;
                }
                else {
                    return gameState.GetPlayerSide().Count;
                }
            case AttributeType.NUM_ENEMY:
                if (IsEnemy(role))
                {
                    return gameState.GetPlayerSide().Count;
                }
                else
                {
                    return gameState.GetEnemySide().Count;
                }
            default:
                return float.MinValue;
        }
    }

}
