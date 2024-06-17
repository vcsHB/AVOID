using System;
using System.Collections;
using ObjectPooling;
using UnityEngine;

public class NormalCannon : FieldObject
{
    [Header("Cannon Setting")] 
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _projectileSpeed = 5;
    [SerializeField] private int _shootAmount;
    [SerializeField] private float _shootCoolTime;
    [SerializeField] private float _attackStateCoolTime;
    [SerializeField] private PoolingType _projectile;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private bool _isAutoFire = true;
    private bool _isAttackState;
    
    [Header("Essential Setting")]
    [SerializeField] private Transform _cannonHeadTrm;
    private Transform _gunTipTrm;
    private ParticleSystem _shootParticle;
    
    private float _currentTime = 0;
    private WaitForSeconds ws;
    private Vector3 _attackDirection;
    protected override void Awake()
    {
        base.Awake();
        ws = new WaitForSeconds(_shootCoolTime);
        _gunTipTrm = _cannonHeadTrm.Find("GunTip");
        _shootParticle = _cannonHeadTrm.Find("ShootParticle").GetComponent<ParticleSystem>();
        _attackDirection = (_gunTipTrm.position - _cannonHeadTrm.position).normalized;
    }
    
    private void Update()
    {
        if (!_isAutoFire) return;
        
        if (!_isAttackState)
            _currentTime += Time.deltaTime;
        else
            return;

        if (_currentTime >= _attackStateCoolTime)
        {
            _currentTime = 0;
            _isAttackState = true;
            StartCoroutine(AttackStateCoroutine());
        }
        
    }

    private IEnumerator AttackStateCoroutine()
    {
        
        for (int i = 0; i < _shootAmount; i++)
        {
            yield return ws;
            Fire();
        }

        _isAttackState = false;
    }

    public void Fire()
    {
        _shootParticle.Play();
        Projectile projectile = PoolManager.Instance.Pop(_projectile) as Projectile;
        
        projectile.Fire(_gunTipTrm.position, _attackDirection, _damage, _projectileSpeed);
    }

    public override void ResetItem()
    {
        _currentTime = 0;
    }
}
