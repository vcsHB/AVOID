using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class OptionObject : MonoBehaviour
{
    public string optionName;
    [ColorUsage(true,true)] public Color optionColor;
    public UnityEvent OnSelectEvent;
    
    public void Select()
    {
        OnSelectEvent?.Invoke();    
    }
    
    
}
