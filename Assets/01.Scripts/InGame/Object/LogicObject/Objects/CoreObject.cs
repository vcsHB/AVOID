using System;
using UnityEngine;
using UnityEngine.Events;

public class CoreObject : LogicObject
{
    [SerializeField] private MeshRenderer _lightEffectMesh;


    protected override void Awake()
    {
        base.Awake();
        _lightEffectMesh = transform.Find("EnergyEffect").GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        logicSolvedEvent.AddListener(HandlerSetLight);
    }


    private void HandlerSetLight()
    {
        SetLight(true);
    }
    private void SetLight(bool value)
    {
        _lightEffectMesh.enabled = value;
    }
    
}