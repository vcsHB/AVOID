using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : FieldObject
{
    
    private float _currentTime;
    [SerializeField] private float _attackCoolTime;

    private Transform _cannonHeadTrm;
    private Transform _gunTipTrm;


    protected override void Awake()
    {
        base.Awake();
        _gunTipTrm = _cannonHeadTrm.Find("GunTip");
    }


    public override void ResetItem()
    {
        
    }
}
