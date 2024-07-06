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
    public UnityEvent<int, bool> OnButtonTriggerEvent;
   
    private Tween _currentTween;
    private Coroutine _coroutine;
    private bool _isPressed;
    
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

    public override bool Interact(IInteractable interactable)
    {
        interactEvent?.Invoke(_logicIndex, true);
        return HandleInteraction(interactable);
    }
    
    protected override bool HandleInteraction(IInteractable interactable)
    {
        if (_isPressed)
            return true;
        _coroutine = StartCoroutine(InteractCoroutine());
        return true;
    }

    private IEnumerator InteractCoroutine()
    {
        print("버튼 눌림");
        _isPressed = true;
        SetButton(true);
        yield return new WaitForSeconds(_buttonHoldDuration);
        isActive = true;

        _collider.enabled = false;
        OnButtonTriggerEvent?.Invoke(_logicIndex, true);

    }

    private void SetButton(bool value)
    {
        // if(_currentTween != null)
        //     _currentTween.Complete();
        if (value)
        {
            _currentTween.Complete();
            _currentTween = _buttonPanel.DOLocalMoveY(_buttonOnPosY, _buttonHoldDuration);
        }
        else
        {
            StopCoroutine(_coroutine);
            _isPressed = false;
            _currentTween.Complete();
            _currentTween = _buttonPanel.DOLocalMoveY(_buttonDefaultPosY, _buttonHoldDuration);
        }
        
    }

    protected override void DetectTarget()
    {
        Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, _detectRadius, _detectLayer);
        if (hit.collider != null)
        {
            if (isActive == false)
            {
                hit.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);
                
            }
            return;
        }
        //print("버튼 빠짐");
        SetButton(false);
        _collider.enabled = true;
        isActive = false;
    }

}