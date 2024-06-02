using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Cannon : FieldObject
{
    [Header("Cannon Setting")] 
    [SerializeField] private float _attackCoolTime;
    [SerializeField] private float _shootPower;
    [SerializeField] private float _targetDetectRadius = 7f;
    [SerializeField] private LayerMask _targetLayer;

    [Header("Targeting Setting")]    
    [SerializeField] private float _areaSize = 1.5f;
    [SerializeField] private TargetArea _targetArea;
    [SerializeField] private float _targetingSpeed = 3f;
    [SerializeField] private float _targetingDuration = 5f;    

   
    [Header("Essential Setting")]
    [SerializeField] private Transform _cannonHeadTrm;
    private Transform _gunTipTrm;
    private bool _isTargetDetected;
    private Collider[] hits;
    private Transform _targetTrm;
    private float _currentTime = 0;
    private bool _isCoolTimed;


    protected override void Awake()
    {
        base.Awake();
        _gunTipTrm = _cannonHeadTrm.Find("GunTip");
        hits = new Collider[1];
    }

    private void Update()
    {
        if (!_isCoolTimed)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > _attackCoolTime)
                _isCoolTimed = true;

            return;
        }

        if (DetectTarget())
        {
            _targetArea.SetArea(true);
            FollowTargetArea();
        }
        else
        {
            _targetArea.SetArea(false);
        }
        
        
        
    }

    private bool DetectTarget()
    {
        if (!_isTargetDetected)
        {
            int amount = Physics.OverlapSphereNonAlloc(transform.position, _targetDetectRadius, hits, _targetLayer);
            if (amount > 0)
            {
                _isTargetDetected = true;
                _targetTrm = hits[0].transform;
                TargetingStart();
                return true;

            }

            return false;
        }
        Vector3 direction = _targetTrm.position - transform.position;
        if (direction.magnitude > _targetDetectRadius)
        {
            _isTargetDetected = false;
            _targetTrm = null;
            StopAllCoroutines();
            return false;
        }

        return true;
    }

    private void TargetingStart()
    {
        _targetArea.SetArea(_areaSize);
        StartCoroutine(TargetingCoroutine());
    }

    private IEnumerator TargetingCoroutine()
    {
        float currentTime = 0;
        while (currentTime < _targetingDuration)
        {
            yield return null;
        }   
    }

    private void FollowTargetArea()
    {
        if(_targetTrm == null) print("타겟이 null임");
        Vector3 direction = _targetTrm.position - _targetArea.transform.position;
                
        
        _targetArea.transform.position += (direction * _targetingSpeed * Time.deltaTime);

    }

    private void Fire()
    {
        
    }
    

    public override void ResetItem()
    {
        
    }
}
