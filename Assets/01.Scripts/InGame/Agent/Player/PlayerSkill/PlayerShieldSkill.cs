using System.Collections;
using UnityEngine;

public class PlayerShieldSkill: PlayerSkill
{
    [SerializeField] private float _shieldDuration = 5;

    public override bool UseSkill()
    {
        if (!base.UseSkill())
            return false;
        
        StartCoroutine(ShieldCoroutine());
        return true;
    }

    private IEnumerator ShieldCoroutine()
    {
        player.PlayerVFXCompo.SetShield(true);
        player.Stat.IsResist = true;
        yield return new WaitForSeconds(_shieldDuration);
        player.PlayerVFXCompo.SetShield(false);
        player.Stat.IsResist = false;
    }
}