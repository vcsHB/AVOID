using UnityEngine;

public abstract class WindowPanel : MonoBehaviour
{
    [SerializeField] protected RectTransform _rectTrm;
    [SerializeField] protected bool _isActive;
    
    
    [SerializeField] protected Vector2 _defaultPosition;
    [SerializeField] protected Vector2 _targetPosition;
    [SerializeField] protected float _onOffTime;

    public bool IsActive => _isActive;
    
    
    protected virtual void Awake()
    {
        _rectTrm = transform as RectTransform;
    }

    public abstract void ShowUI();
    public abstract void DisableUI();


}
