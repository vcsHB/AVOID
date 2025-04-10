using UnityEngine;

public class MoveCountTriggerObject : SkillTriggerObject
{
    [SerializeField] private int countLimit = 5;
    public override void HandleSkillTrigger()
    {
        for (int i = 0; i < _triggerSkills.Length; i++)
        {
            PlayerSkill skill = PlayerSkillManager.Instance.GetSkill(_triggerSkills[i]);
            skill.UseSkill();
            (skill as PlayerMoveCountSkill).SetCount(countLimit);
        }
        print($"이동제한 {countLimit}으로 초기화");
    }

    public void SetCount(int amount)
    {
       
        countLimit = amount;
    }
}
