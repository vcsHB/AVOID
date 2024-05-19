using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public abstract class Agent : MonoBehaviour, IInteractable
{
    [SerializeField] protected float _moveTime = 0.5f;
    [SerializeField] protected float _moveCell = 2.5f;
    [SerializeField] protected float _jumpScale = 0.5f;
    [SerializeField] protected Transform _visualTrm;
    [SerializeField] private Vector3 _boxCastSize = Vector3.one;
    public Vector3 MoveDirection { get; set; }
    

    protected bool _isStun;
    protected bool _isMoving;
    protected float _currentMoveTime = 0;

    protected Quaternion _targetRotate;
    protected Vector3 _targetPos;
    
    protected Rigidbody _rigid;
    [SerializeField] private LayerMask _objectLayer;

    protected virtual void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }
    
    protected virtual void Update()
    {
        if (_isStun || TimeManager.TimeScale == 0)
        {
            return;
        }
        
        if (_isMoving)
        {

            _currentMoveTime += Time.deltaTime;
            float ratio = _currentMoveTime / _moveTime;
            //transform.position = Vector3.Lerp(_beforePosition, _targetPos, ratio);
            _visualTrm.rotation = Quaternion.Slerp(Quaternion.identity, _targetRotate, EasingFunction.EaseInCircle(ratio));
            if (ratio >= 1f)
            {
                _currentMoveTime = 0;
                //_visualTrm.rotation = Quaternion.identity;
                _isMoving = false;
            }
        }
    }

    
    protected virtual bool Move(Vector3 direction)
    {
        if (_isMoving) return false;
        
        int x = Mathf.Clamp((int)(direction.x), -1, 1);
        int z = Mathf.Clamp((int)(direction.z), -1, 1);
        MoveDirection = new Vector3(
            x,
            0,
            x == 0 ? z : 0
            ).normalized * _moveCell;
        if (MoveDirection.magnitude < 0.1f) return false;
        DetectInteraction();

        _targetPos = transform.position + MoveDirection;
        _targetRotate = Quaternion.Euler(MoveDirection.z * 180f, 0, MoveDirection.x * -180f);
        transform.DOJump(_targetPos, _jumpScale, 1, _moveTime);
        _isMoving = true;
        return true;
    }
    
    public void DetectInteraction()
    {
        RaycastHit[] hits = new RaycastHit[5];
        int amount = Physics.BoxCastNonAlloc(transform.position, _boxCastSize, MoveDirection.normalized, hits, Quaternion.identity, 1.5f, _objectLayer);
        if (amount == 0) return;

        for (int i = 0; i < amount; i++)
        {
            if (hits[i].transform.TryGetComponent(out InteractObject interactObject))
            {
                interactObject.Interact(this);
            }
        }
    }

    

}