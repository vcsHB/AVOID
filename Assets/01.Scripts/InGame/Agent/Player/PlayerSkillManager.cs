using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkillEnum
{
    None = 0,
    Shield = 1,
    MoveCount
}

public class PlayerSkillManager : MonoSingleton<PlayerSkillManager>
{
    private Dictionary<Type, PlayerSkill> _skills;
    private List<PlayerSkill> _enableSkillList;

    private void Awake()
    {
        _skills = new Dictionary<Type, PlayerSkill>();
        _enableSkillList = new List<PlayerSkill>();
        foreach(PlayerSkillEnum skillEnum in Enum.GetValues(typeof(PlayerSkillEnum)))
        {
            if (skillEnum == PlayerSkillEnum.None) continue;

            PlayerSkill skillCompo = GetComponent($"Player{skillEnum.ToString()}Skill") as PlayerSkill;
            Type type = skillCompo.GetType();
            _skills.Add(type, skillCompo);
        }
    }

    public void AddEnableSkill(PlayerSkill skill)
    {
        _enableSkillList.Add(skill);
    }

    private void Update()
    {
        foreach (PlayerSkill skill in _enableSkillList)
        {
            skill.UseSkill();
        }

        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     GetSkill(PlayerSkill.TheWorld).UseSkill();
        // }
    }

    public T GetSkill<T>() where T : PlayerSkill
    {
        Type t = typeof(T);
        if (_skills.TryGetValue(t, out PlayerSkill target))
        {
            return target as T;
        }
        return null;
    }

    public PlayerSkill GetSkill(PlayerSkillEnum skillEnum)
    {
        Type type = Type.GetType($"Player{skillEnum.ToString()}Skill"); //이건 리플렉션
        if (type == null) return null;

        if (_skills.TryGetValue(type, out PlayerSkill target))
        {
            return target;
        }

        return null;
    }

}