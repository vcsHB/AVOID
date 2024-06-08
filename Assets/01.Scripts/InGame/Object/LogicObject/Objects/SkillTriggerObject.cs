using UnityEngine;

public class SkillTriggerObject : LogicObject
{
    [SerializeField] private PlayerSkillEnum[] _triggerSkills;

    private void Start()
    {
        logicSolvedEvent.AddListener(HandleSkillTrigger);
    }
    
    public void HandleSkillTrigger()
    {
        for (int i = 0; i < _triggerSkills.Length; i++)
        {
            PlayerSkillManager.Instance.GetSkill(_triggerSkills[i]).UseSkill();
        }
    }
}