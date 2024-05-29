using System;
using ObjectPooling;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    #region Component

    public AgentMovement MovementCompo { get; protected set; }
    public Rigidbody RigidCompo { get; protected set; }
    public Health HealthCompo { get; protected set; }
    public AgentVFX VFXCompo { get; protected set; }

    #endregion

    [Header("Agent Setting Values")] 
    [SerializeField] protected PoolingType _destroyVFX;

    protected virtual void Awake()
    {
        MovementCompo = GetComponent<AgentMovement>();
        RigidCompo = GetComponent<Rigidbody>();
        HealthCompo = GetComponent<Health>();
        VFXCompo = transform.Find("AgentVFX").GetComponent<AgentVFX>();
    }

    protected virtual void Start()
    {
        HealthCompo.OnDieEvent += HandleAgentDie;
        
    }

    private void OnDestroy()
    {
        HealthCompo.OnDieEvent -= HandleAgentDie;
    }

    public virtual void HandleAgentDie()
    { 
        EffectObject effect = PoolManager.Instance.Pop(PoolingType.DestroyVFX) as EffectObject;
        effect.Initialize(transform.position);
    }
}