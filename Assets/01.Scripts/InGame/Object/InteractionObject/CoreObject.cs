using UnityEngine;
using UnityEngine.Events;

public class CoreObject : InteractObject
{
    public UnityEvent interactEvent;
    
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
}