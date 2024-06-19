using UnityEngine;

public delegate void CoolDownInfo(float current, float total);
public class PlayerSkill : MonoBehaviour
{
    public bool skillEnabled = false;
    [SerializeField] protected float _coolDown; // 이 스킬의 쿨타임
    [SerializeField] protected bool _isAutoSkill;

    [HideInInspector] public Player player;
    public event CoolDownInfo CoolDownEvent;

    protected float _cooldownTimer;
    //public LayerMask whatIsEnemy;

    public void UnlockSkill()
    {
        if (skillEnabled) return;
        skillEnabled = true;
        if (_isAutoSkill)
        {
            PlayerSkillManager.Instance.AddEnableSkill(this);
        }
    }

    protected virtual void Start()
    {
        player = PlayerManager.Instance.Player;
    }

    protected virtual void Update()
    {
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0)
            {
                _cooldownTimer = 0;
                
            }
            CoolDownEvent?.Invoke(_cooldownTimer, _coolDown);
        }
    }

    public virtual bool UseSkill()
    {
        if (_cooldownTimer > 0 || !skillEnabled) return false;

        _cooldownTimer = _coolDown;
        return true;
    }

    public virtual void DisableSkill()
    {
        
    }
}