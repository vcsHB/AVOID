using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public PlayerSkill[] playerSkillList;

    private void Awake()
    {
        playerSkillList = GetComponents<PlayerSkill>();
    }
    
    
    
}