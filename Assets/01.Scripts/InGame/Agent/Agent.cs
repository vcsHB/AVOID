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

    private void Awake()
    {
        MovementCompo = GetComponent<AgentMovement>();
        RigidCompo = GetComponent<Rigidbody>();
        HealthCompo = GetComponent<Health>();
        VFXCompo = GetComponent<AgentVFX>();
    }
}