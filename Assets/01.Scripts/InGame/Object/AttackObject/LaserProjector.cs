using System;
using ObjectPooling;
using UnityEngine;

public class LaserProjector : MonoBehaviour
{
    [Header("Laser Setting")] 
    [SerializeField] private bool _laserActive;
    [SerializeField] private int _reflectAmount = 2;
    [SerializeField] private Vector3 _laserSize = Vector3.one;
    [SerializeField] private float _limitDistance = 10f;
    [SerializeField] private PoolingType _collisionParticle;
    private EffectObject[] _collisionEffectObjects;
    
    [Header("Damage Casting Setting")] 
    [SerializeField] private bool _useDamageCast;
    [SerializeField] private float _damageCastCoolTime = 1f;
    [SerializeField] private int _damage = 2;
    [SerializeField] private LayerMask _damageTargetLayer;

    [Header("Setting")] [SerializeField] private bool _isStatic;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _laserFirePosTrm;
    [SerializeField] private Transform _aimTrm;

    private Vector3 _fireDirection;
    private int _pointsAmount;
    private int _laserLineAmount;
    private float _currentTime = 0;
    
    private void Start()
    {
        Initialize();
        
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _collisionEffectObjects.Length; i++)
        {
            if(PoolManager.Instance)
                PoolManager.Instance.Push(_collisionEffectObjects[i]);
        }
    }

    private void Update()
    {
        if (!_laserActive) return;
        
        _currentTime += Time.deltaTime;
        UpdateLaser();
    }

    private void Initialize()
    {
        _lineRenderer.positionCount = _reflectAmount + 2;
        _pointsAmount = _reflectAmount + 2;
        _laserLineAmount = _reflectAmount + 1;
        
        _lineRenderer.SetPosition(0, _laserFirePosTrm.position);
        _collisionEffectObjects = new EffectObject[_laserLineAmount];
        for (int i = 0; i < _laserLineAmount; i++)
        {
            _collisionEffectObjects[i] = PoolManager.Instance.Pop(_collisionParticle) as EffectObject;
            _collisionEffectObjects[i].SetPosition(_lineRenderer.GetPosition(i+1));
            _collisionEffectObjects[i].Play();
        }
    }
    
    private void UpdateLaser()
    {
        if (_useDamageCast && _currentTime >= _damageCastCoolTime)
        {
            if(DamageCast())
                _currentTime = 0;
        }
        
        if (!_isStatic)
        {
            _fireDirection = (_aimTrm.position - _laserFirePosTrm.position).normalized;
            UpdateLaserPos();
        }


    }

    private bool DamageCast()
    {
        for (int i = 0; i < _laserLineAmount; i++)
        {
            Vector3 origin = _lineRenderer.GetPosition(i);
            Vector3 direction = _lineRenderer.GetPosition(i + 1) - origin;
            //RaycastHit[] hits = Physics.RaycastAll(origin, direction.normalized, direction.magnitude, _damageTargetLayer);
            RaycastHit[] hits = new RaycastHit[2];
            int amount = Physics.BoxCastNonAlloc(origin, _laserSize, direction.normalized, hits,
                Quaternion.LookRotation(direction.normalized), direction.magnitude);
            if (amount == 0) return false;
            
            for (int j = 0; j < amount; j++)
            {
                if (hits[j].transform.TryGetComponent(out Health health))
                {
                    health.TakeDamage(_damage);
                }
            }
        }

        return true;
    }

    private void UpdateLaserPos()
    {
        _lineRenderer.SetPosition(0, _laserFirePosTrm.position);
        Vector3 origin = _lineRenderer.GetPosition(0);
        Vector3 direction = _fireDirection;
        for (int i = 0; i < _laserLineAmount; i++)
        {
            direction = _lineRenderer.GetPosition(i + 1) - origin;
            RaycastHit[] hits = Physics.RaycastAll(origin, direction.normalized, direction.magnitude, _damageTargetLayer);
            
            
            //RaycastHit[] hits = new RaycastHit[2];
            int amount = Physics.BoxCastNonAlloc(origin, _laserSize, direction.normalized, hits,
                Quaternion.LookRotation(direction.normalized), direction.magnitude);
            if (amount == 0) return;
            
            for (int j = 0; j < amount; j++)
            {
                if (hits[j].transform.TryGetComponent(out Health health))
                {
                    health.TakeDamage(_damage);
                }
            }
        }
    }
}
