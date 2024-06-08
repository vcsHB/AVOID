using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCoreObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer _energyBendMeshRenderer;
    [SerializeField] private int _shieldAmount = 2;

    private int _shieldMaterialHash;

    private void Awake()
    {
        _shieldMaterialHash = Shader.PropertyToID("_Amount");

    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        SetShieldAmount();
    }
    

    public void DecreaseShield()
    {
        _shieldAmount--;
        SetShieldAmount();
    }

    private void SetShieldAmount()
    {
        if (_shieldAmount <= 0)
            _energyBendMeshRenderer.enabled = false;
        else
            _energyBendMeshRenderer.enabled = true;
        
        _energyBendMeshRenderer.material.SetInt(_shieldMaterialHash, _shieldAmount);
        
    }
}
