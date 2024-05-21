using System;
using UnityEngine;
using UnityEngine.Events;

public class CoreObject : InteractObject
{
    public UnityEvent interactEvent;

    [SerializeField] private float _speed = 2;
    
    protected override void Update()
    {
        if (canInteract)
        {
            
        }
    }

    protected override void DetectTarget()
    {
        
        
    }


    public override void Interact(IInteractable interactable)
    {
        
        interactEvent?.Invoke();
    }

    private void Blink()
    {
        
    }
}