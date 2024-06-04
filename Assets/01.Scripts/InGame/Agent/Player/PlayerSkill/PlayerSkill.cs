using UnityEngine;

public delegate void CoolDownInfo(float current, float total);
public abstract class PlayerSkill : MonoBehaviour
{
    public bool skillEnabled = false;
    [SerializeField] protected float _coolDown; // 이 스킬의 쿨타임
    [SerializeField] protected bool _isAutoSkill;

    [HideInInspector] public Player player;
    public event CoolDownInfo CoolDownEvent;

    protected float _cooldownTimer;
    
}