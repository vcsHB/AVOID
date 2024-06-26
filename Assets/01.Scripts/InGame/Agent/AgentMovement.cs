﻿using System.Runtime.InteropServices.WindowsRuntime;
using DG.Tweening;
using UnityEngine;

public class AgentMovement : MonoBehaviour, IInteractable
{
    protected Agent _agent;
    
    [SerializeField] protected float _moveTime = 0.5f;
    [SerializeField] protected float _moveCell = 2.5f;
    [SerializeField] protected float _jumpScale = 0.5f;
    [SerializeField] protected Transform _visualTrm;
    [SerializeField] private Vector3 _boxCastSize = Vector3.one;

    protected bool _isStun;
    protected bool _isMoving;
    protected float _currentMoveTime = 0;

    protected Quaternion _targetRotate;
    protected Vector3 _targetPos;
    
    [SerializeField] private LayerMask _objectLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    public Vector3 MoveDirection { get; set; }

    protected virtual void Awake()
    {
        _agent = GetComponent<Agent>();
       
    }
    
    protected virtual void Update()
    {
        if (_isStun)
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

    
    public virtual bool Move(Vector3 direction)
    {
        if (_isMoving) return false;
        
        int x = Mathf.Clamp((int)(direction.x), -1, 1);
        int z = Mathf.Clamp((int)(direction.z), -1, 1);
        Vector3 totalDirection = new Vector3(
            x,
            0,
            x == 0 ? z : 0
        ).normalized * _moveCell;
        MoveDirection = totalDirection;
        if (MoveDirection.magnitude < 0.1f) return false;
        if (!DetectInteraction()) return false;

        _targetPos = transform.position + totalDirection;
        _targetRotate = Quaternion.Euler(totalDirection.z * 180f, 0, totalDirection.x * -180f);
        transform.DOJump(_targetPos, _jumpScale, 1, _moveTime);
        _isMoving = true;
        return true;
    }

    public bool DetectObstacle()
    {
        RaycastHit[] hits = new RaycastHit[5];
        //Collider[] colliders = new Collider[5];
        int amount = Physics.BoxCastNonAlloc(transform.position, _boxCastSize, MoveDirection.normalized, hits, Quaternion.identity, 4f, _obstacleLayer);
        //int obstacleAmount = Physics.OverlapSphereNonAlloc(_targetPos, 2, colliders, _obstacleLayer);
        return amount == 0;
    }


    public bool DetectInteraction()
    {
        RaycastHit[] hits = new RaycastHit[5];
        int amount = Physics.BoxCastNonAlloc(transform.position, _boxCastSize, MoveDirection.normalized, hits, Quaternion.identity, 4f, _objectLayer);
        if (amount == 0) return true;

        for (int i = 0; i < amount; i++)
        {
            if (hits[i].transform.TryGetComponent(out InteractObject interactObject))
            {
                if (!interactObject.Interact(this)) return false;
            }
        }

        return true;
    }

    public void SetStun(bool value)
    {
        _isStun = value;
    }

    public void SetDefaultRotate()
    {
        _visualTrm.rotation = Quaternion.identity;
    }

}