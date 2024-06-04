using System;
using ObjectPooling;
using UnityEngine;

public abstract class Projectile : PoolableMono
{
    [Header("Projectile Setting")]
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed = 4f;
    [SerializeField] protected float _lifeTime = 10f;
    [SerializeField] protected Transform _visualTrm;
    [Header("Range Setting")]
    [SerializeField] protected bool _isRangeDamage;
    [SerializeField] protected LayerMask _targetLayer;
    [SerializeField] protected int _limitTargetCount = 2;
    [SerializeField] protected float _attackRange = 3;
    [SerializeField] protected PoolingType _destroyVFX;
    
    protected Vector3 _direction;

    protected Rigidbody _rigid;


    protected virtual void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    

    
    public virtual void Fire(Vector3 firePos, Vector3 direction, int damage = 5)
    {
        transform.position = firePos;
        _damage = damage;
        _direction = direction;
        transform.forward = _direction;
        _rigid.velocity = _direction * _speed;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        EffectObject vfx = PoolManager.Instance.Pop(_destroyVFX) as EffectObject;
        vfx.Initialize(transform.position);
        vfx.Play();
        
        if (_isRangeDamage)
        {
            RangeDamage();
            
            PoolManager.Instance.Push(this);
            
            return;
        }
        
        if (other.TryGetComponent(out Health target))
        {
            target.TakeDamage(_damage);
        }
        PoolManager.Instance.Push(this);
    }

    protected virtual void RangeDamage()
    {
        Collider[] hits = new Collider[_limitTargetCount];
        int amount = Physics.OverlapSphereNonAlloc(transform.position, _attackRange, hits, _targetLayer);
        if (amount == 0) return;
        for (int i = 0; i < amount; i++)
        {
            if(hits[i].TryGetComponent(out Health rangeTarget))
            {
                rangeTarget.TakeDamage(_damage);
            }
        }
    }
}