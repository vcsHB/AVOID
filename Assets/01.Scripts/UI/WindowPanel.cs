using UnityEngine;

public abstract class WindowPanel : MonoBehaviour
{
    [SerializeField] protected RectTransform _rectTrm;
    [SerializeField] protected bool _isActive;
    public bool IsActive => _isActive;
    
    
    protected virtual void Awake()
    {
        _rectTrm = transform as RectTransform;
    }

    public abstract void ShowUI();
    public abstract void DisableUI();


}
