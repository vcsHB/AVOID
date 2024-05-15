using UnityEngine;
using UnityEngine.Events;

public class ButtonObject : InteractObject
{
    [SerializeField] private Transform _buttonPanel;
    public UnityEvent OnButtonTriggerEvent;
    
    public override void Interact(Agent agent)
    {
        OnButtonTriggerEvent?.Invoke();
    }

    private void SetButton(bool value)
    {
        
    }
}