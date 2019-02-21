using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ActionUtils : MonoBehaviour {

    private static ActionUtils _instance;
    public static ActionUtils Instance { get { return _instance; } }

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
    }

    public bool ShouldTakeDamage(Role target, float damage, ActionType damageType)
    {
        if (!target.isAlive)
        {
            return false;
        }
        return true;
    }


    public Action CreateAction(string name, Role from, Role to, ActionConfig config) {
        GameObject actionHolder = PrafabUtils.Instance.create("ActionHolder");
        Type type = Type.GetType(name);
        actionHolder.AddComponent(type);

        Action action = actionHolder.GetComponent<Action>();
        action.Init(from, to, config);

        return action;
    }

    public void SaveActionConfigToJson(Action action)
    {
        string json = JsonUtility.ToJson(action.config);
        File.WriteAllText(Application.dataPath + "/Resources/data/actions/" + action.actionName.Replace(' ', '_') + ".json", json.ToString());
    }

    public ActionConfig LoadAction(string name) {
        ActionConfig config = null;
        string filePath = Path.Combine(Application.dataPath + "/Resources/data/actions/", name + ".json");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            config = JsonUtility.FromJson<ActionConfig>(dataAsJson);
            //Debug.Log(loadedData);
        }
        return config;
    }
}
