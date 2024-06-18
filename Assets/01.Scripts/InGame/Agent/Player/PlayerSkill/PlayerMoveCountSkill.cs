using UnityEngine;

public class PlayerMoveCountSkill : PlayerSkill
{
    [SerializeField] private int _count = 0;
    private bool _isActive;
    protected override void Start()
    {
        base.Start();
        player.PlayerMovementCompo.OnMovementEvent += MoveTrigger;
    }

    public override bool UseSkill()
    {
        if (!base.UseSkill())
            return false;

        _isActive = true;
        return true;
    }

    public override void DisableSkill()
    {
        _isActive = false;
        player.PlayerVFXCompo.SetMoveCountPanel(false);
    }

    private void MoveTrigger()
    {
        if (!_isActive)
            return;
        _count--;
        player.PlayerVFXCompo.SetMoveCountPanel(true, _count);
        if (_count <= 0)
        {
            player.HealthCompo.TakeDamage(99);
            DisableSkill();
        }
    }

    public void SetCount(int count)
    {
        if (!_isActive)
            return;
        _count = count;
        player.PlayerVFXCompo.SetMoveCountPanel(true, count);
    }
    
    
}