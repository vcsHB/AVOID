using System;
using UnityEngine;

public abstract class WindowPanel : MonoBehaviour
{
    [SerializeField] protected RectTransform _rectTrm;
    [SerializeField] protected bool _isActive;
    
    
    [SerializeField] protected Vector2 _defaultPosition;
    [SerializeField] protected Vector2 _targetPosition;
    [SerializeField] protected float _onOffTime;

    public Action OnDisableEvent;
    
    public bool IsActive => _isActive;
    protected CanvasGroup _canvasGroup;
    
    
    protected virtual void Awake()
    {
        _rectTrm = transform as RectTransform;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public abstract void ShowUI();
    public abstract void DisableUI();


    protected void SetVisible(bool value)
    {
        _canvasGroup.alpha = value ? 1 : 0;
        _canvasGroup.interactable = value;
        if(value)
            OnDisableEvent?.Invoke();
    }

}
