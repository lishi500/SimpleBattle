using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStrategy : MonoBehaviour {
    StrategyConfig config;

    public List<Skill> NextSkill(Role self, Role to, int round) {
        List<Skill> skills = new List<Skill>();
        if (config.exclusiveSkill.Length > 0 && hasMatchRound(round, config.exclusiveSkillRound).Count > 0)
        {
            List<Skill> exclusiveSkills = retriveAllMatchedSkills(config.exclusiveSkill, hasMatchRound(round, config.exclusiveSkillRound));
            skills.AddRange(exclusiveSkills);
        }
        else {
            if (config.inclusiveSkill.Length > 0 && hasMatchRound(round, config.inclusiveSkillRound).Count > 0) {
                List<Skill> inclusiveSkills = retriveAllMatchedSkills(config.inclusiveSkill, hasMatchRound(round, config.inclusiveSkillRound));
                skills.AddRange(inclusiveSkills);
            }
            skills.Add(SkillUtils.Instance.CreateSkill(config.basicSkill));
        }

        return skills;
    }

    private List<int> hasMatchRound(int round, int[] rounds) {
        List<int> matches = new List<int>();
        if (rounds.Length > 0) {
            for (int i = 0; i < rounds.Length; i++) {
                if (round % rounds[i] == 0) {
                    matches.Add(i);
                }
            }
        }

        return matches;
    }

    private List<Skill> retriveAllMatchedSkills(string[] skillNames, List<int> matchedIndex) {
        List<Skill> skills = new List<Skill>();
        foreach (int i in matchedIndex) {
            string skillName = skillNames[i];
            skills.Add(SkillUtils.Instance.CreateSkill(skillName));
        }

        return skills;
    }
}
