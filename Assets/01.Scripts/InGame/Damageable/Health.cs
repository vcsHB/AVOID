using System;
using UnityEngine;

public delegate void OnValueChanged(int currentValue, int maxValue);
public class Health : MonoBehaviour, IDamageable
{
    public event OnValueChanged OnHealthChanged;
    public event Action OnDieEvent;
    
    [field: SerializeField] public int hp { get; private set; }
    public int maxHp { get; private set; }
    
    
    public void TakeDamage(int damage)
    {
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