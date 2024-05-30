using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : FieldObject
{
    [Header("Cannon Setting")] 
    [SerializeField] private float _attackCoolTime;
    [SerializeField] private float _shootPower;
    [SerializeField] private float _playerDetectRadius = 7f;
    [SerializeField] private LayerMask _targetLayer;

    private float _currentTime = 0;
    private bool _isCoolTimed;

    [Header("Essential Setting")]
    [SerializeField] private Transform _cannonHeadTrm;
    private Transform _gunTipTrm;
    [SerializeField] private TargetArea _targetArea;

    
    private bool _isPlayerDetected;
    

    protected override void Awake()
    {
        base.Awake();
        _gunTipTrm = _cannonHeadTrm.Find("GunTip");
    }

    private void Update()
    {
        if (!_isCoolTimed)
        {
            _currentTime += Time.deltaTime;
        }
        
    }

    private void DetectPlayer()
    {
        
    }

    private void Fire()
    {
        
    }
    

    public override void ResetItem()
    {
        
    }
}
