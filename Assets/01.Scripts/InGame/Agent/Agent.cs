using System;
using System.Collections;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    [SerializeField] protected float _moveTime = 0.5f;
    [SerializeField] protected float _moveCell = 2.5f;
    protected bool _isStun;
    protected bool _isMoving;
    protected float _currentMoveTime = 0;


    protected Vector3 _moveDirection;
    protected Vector3 _beforePosition;
    protected Vector3 _targetPos;
    
    protected Rigidbody _rigid;

    protected virtual void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        if (_isStun || TimeManager.TimeScale == 0)
        {
            return;
        }

        if (_isMoving)
        {
            
            _currentMoveTime += Time.deltaTime;
            float ratio = _currentMoveTime / _moveTime;
            transform.position = Vector3.Lerp(_beforePosition, _targetPos, ratio);
            if (ratio >= 1f)
            {
                _currentMoveTime = 0;
                _isMoving = false;
            }
        }
    }

    
    protected virtual void Move(Vector3 direction)
    {
        if (_isMoving) return;
        
        _beforePosition = transform.position;
        int x = Mathf.Clamp((int)(direction.x), -1, 1);
        int z = Mathf.Clamp((int)(direction.z), -1, 1);
        _moveDirection = new Vector3(
            x,
            0,
            x == 0 ? z : 0
            ) * _moveCell;
        print(_moveDirection);
        if (_moveDirection.magnitude < 0.1f) return;
        _targetPos = transform.position + _moveDirection;
        
        _isMoving = true;
    }

}