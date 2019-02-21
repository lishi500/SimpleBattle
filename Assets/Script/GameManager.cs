using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    [HideInInspector] public GameState gameState;
    [HideInInspector] public PrefabHolder prefabHolder;
    [HideInInspector] public RoundController roundController;

    GameObject damageNew;
    GameObject fireBall;
    public Canvas canvas;


    Vector2 start;
    Vector2 end;
    float height = 50;
    float time = 0;

    private void Awake()
    {
        instance = this;
        prefabHolder = this.GetComponent<PrefabHolder>();
    }

    void GameControll() {
        Role next = roundController.SwitchControl();
        if (!RoleUtils.Instance.IsPlayer(next)) {
            next.NextAction();
        }
    }

  
    public void RoleControlFinished(Role role) {
        GameControll();
    }

    // Use this for initialization
    void Start () {
        gameState = FindObjectOfType<GameState>();
        roundController = FindObjectOfType<RoundController>();

        string datapath = "Resources/data/skill.json";
        string filePath = Path.Combine(Application.dataPath, datapath);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            SkillConfig loadedData = JsonUtility.FromJson<SkillConfig>(dataAsJson);
            Debug.Log(loadedData);
        }
        else
        {
            Debug.Log("Not exists");
        }

        //ActionConfig actionConfig = ActionUtils.Instance.LoadAction("GenericAction");
        


        GameControll();
    }

    public void NextRound() {
        roundController.NextRound();
    }


    // Update is called once per frame
    void Update () {
        
    }

}
