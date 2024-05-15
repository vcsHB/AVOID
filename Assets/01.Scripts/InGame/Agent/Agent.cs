using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    [SerializeField] protected float _moveTime = 0.5f;
    [SerializeField] protected float _moveCell = 2.5f;
    [SerializeField] protected float _jumpScale = 0.5f;
    [SerializeField] protected Transform _visualTrm;
    
    protected bool _isStun;
    protected bool _isMoving;
    protected float _currentMoveTime = 0;

    [HideInInspector]
    public Vector3 moveDirection;
    protected Quaternion _targetRotate;
    protected Vector3 _targetPos;
    
    protected Rigidbody _rigid;

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

    
    protected virtual void Move(Vector3 direction)
    {
        if (_isMoving) return;
        
        int x = Mathf.Clamp((int)(direction.x), -1, 1);
        int z = Mathf.Clamp((int)(direction.z), -1, 1);
        moveDirection = new Vector3(
            x,
            0,
            x == 0 ? z : 0
            ) * _moveCell;
        //print(moveDirection);
        if (moveDirection.magnitude < 0.1f) return;
        _targetPos = transform.position + moveDirection;
        _targetRotate = Quaternion.Euler(moveDirection.z * 180f, 0, moveDirection.x * -180f);
        transform.DOJump(_targetPos, _jumpScale, 1, _moveTime);
        _isMoving = true;
    }

    

}