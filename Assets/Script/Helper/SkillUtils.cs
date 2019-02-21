using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkillUtils : MonoBehaviour
{

    private static SkillUtils _instance;
    public static SkillUtils Instance { get { return _instance; } }

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

    public Skill CreateSkill(string name, Role from, Role to, SkillConfig config)
    {
        GameObject skillHolder = new GameObject();
        Type type = Type.GetType(name);
        skillHolder.AddComponent(type);

        Skill skill = skillHolder.GetComponent<Skill>();
        skill.Init(from, to, config);

        return skill;
    }

    public Skill CreateSkill(string name)
    {
        GameObject skillHolder = new GameObject();
        Type type = Type.GetType(name);
        skillHolder.AddComponent(type);

        Skill skill = skillHolder.GetComponent<Skill>();

        return skill;
    }

    public void SaveSillConfigToJson(Skill skill)
    {
        string json = JsonUtility.ToJson(skill);
        File.WriteAllText(Application.dataPath + "/Resources/data/skills/" + skill.config.skillName.Replace(' ', '_')+ ".json", json.ToString());
    }
}
