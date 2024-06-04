using System;
using UnityEngine;

public delegate void OnValueChanged(int currentValue, int maxValue);
public class Health : MonoBehaviour, IDamageable
{
    public event OnValueChanged OnHealthChanged;
    public event Action OnDieEvent;
    
    [field: SerializeField] public int hp { get; private set; }
    public int maxHp { get; private set; }
    
    private Agent _owner;
    private Rigidbody _rigid;
    public void Initialize(Agent agent)
    {
        _owner = agent;
        //actionData = new ActionData();
        hp = _owner.Stat.Health; //  최대체력으로 세팅

    }
    
    public void TakeDamage(int damage)
    {
        if(_owner.Stat.IsResist) return;
        hp -= damage;
        OnHealthChanged?.Invoke(hp, maxHp);
        CheckDie();
    }

    public void RestoreHealth(int amount)
    {
        hp += amount;
        OnHealthChanged?.Invoke(hp, maxHp);
    }

    private void CheckDie()
    {
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDieEvent?.Invoke();
    }
    
}