using ObjectPooling;
using SoundManage;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    #region Component

    public AgentMovement MovementCompo { get; protected set; }
    public Rigidbody RigidCompo { get; protected set; }
    public Health HealthCompo { get; protected set; }
    public AgentVFX VFXCompo { get; protected set; }
    public SoundObject SoundCompo { get; protected set; }

    #endregion

    [Header("Agent Setting Values")] 
    [SerializeField] protected PoolingType _destroyVFX;

    [SerializeField] private AgentStat _defaultStat;
    [field: SerializeField] public AgentStat Stat { get; protected set; }

    protected virtual void Awake()
    {
        Stat = Instantiate(_defaultStat);
        MovementCompo = GetComponent<AgentMovement>();
        RigidCompo = GetComponent<Rigidbody>();
        HealthCompo = GetComponent<Health>();
        SoundCompo = GetComponent<SoundObject>();
        HealthCompo.Initialize(this);
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
        effect.Play();
    }
}