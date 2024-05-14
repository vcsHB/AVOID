using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractObject : MonoBehaviour
{
    [SerializeField] protected float _interactCoolTime;
    public bool isActive;
    public bool canInteract = true;    
    protected float _currentTime = 0;


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (isActive || !canInteract) return;
        Interact();
        
    }

    protected virtual void Update()
    {
        if (canInteract || TimeManager.TimeScale == 0)
        {
            return;
        }
        
        _currentTime += TimeManager.TimeScale * Time.deltaTime;
        if (_currentTime >= _interactCoolTime)
        {
            canInteract = true;
        }
    }


    public abstract void Interact();




}
