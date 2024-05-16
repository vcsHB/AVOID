using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ButtonObject : InteractObject
{
    [SerializeField] private Transform _buttonPanel;
    [SerializeField] private float _buttonHoldDuration = 1f;
    [SerializeField] private float _buttonDefaultPosY;
    [SerializeField] private float _buttonOnPosY;
    public UnityEvent OnButtonTriggerEvent;
   
    private Tween _currentTween;
    
    // private void OnTriggerExit(Collider other)
    // {
    //     print("버튼 빠짐");
    //     SetButton(false);
    //     isActive = false;
    // }

    protected override void Update()
    {
        
        base.Update();
        if (isActive)
        {
            DetectTarget();
            
        }
        
    }

    public override void Interact(IInteractable interactable)
    {
        print("버튼 눌림");
        StartCoroutine(InteractCoroutine());
    }

    private IEnumerator InteractCoroutine()
    {
        SetButton(true);
        yield return new WaitForSeconds(_buttonHoldDuration);
        isActive = true;
        _collider.enabled = false;
        OnButtonTriggerEvent?.Invoke();

    }

    private void SetButton(bool value)
    {
        // if(_currentTween != null)
        //     _currentTween.Complete();
        if (value)
        {
            _currentTween.Complete();
            _currentTween = _buttonPanel.DOMoveY(_buttonOnPosY, _buttonHoldDuration);
        }
        else
        {
            _currentTween.Complete();
            _currentTween = _buttonPanel.DOMoveY(_buttonDefaultPosY, _buttonHoldDuration);
        }
        
    }

    protected override void DetectTarget()
    {
        Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 1.5f, _detectLayer);
        if (hit.collider != null)
        {
            return;
        }
        print("버튼 빠짐");
        SetButton(false);
        _collider.enabled = true;
        isActive = false;
    }

    public override void ResetItem()
    {
        canInteract = true;
        isActive = false;
        _currentTime = 0;
    }
}