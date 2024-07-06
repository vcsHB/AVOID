using System;
using ObjectPooling;
using UnityEngine;

public class ExplodeObject : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private PoolingType _explodeParticlePoolType;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void Explode()
    {
        Collider[] hits = new Collider[5];
        _collider.enabled = false;
        int amount = Physics.OverlapSphereNonAlloc(transform.position, _range, hits, _targetLayer);
        _collider.enabled = true;
        
        if (amount == 0) return;
        for (int i = 0; i < amount; i++)
        {
            if (hits[i].TryGetComponent(out IDamageable health))
            {
                health.TakeDamage(_damage);
            }
        }

        EffectObject effectObject = PoolManager.Instance.Pop(_explodeParticlePoolType) as EffectObject;
        effectObject.Initialize(transform.position);
        effectObject.Play();
        Destroy(gameObject);
    }

}
