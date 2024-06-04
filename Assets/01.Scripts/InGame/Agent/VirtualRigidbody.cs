using System;
using UnityEngine;

public class VirtualRigidbody : MonoBehaviour
{
    [SerializeField] private Vector3 _gravityDirection;
    [SerializeField] private float _gravityAcceleration = 9.8f;
    [SerializeField] private bool _useDeadZone;
    [SerializeField] private float _deadZoneY = -20f;

    [SerializeField] private bool _useGravity = true;
    
    private Rigidbody _rigid;
    public Vector3 velocity;
    private Vector3 _currentVelocity;
    private bool _isStop;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (TimeManager.TimeScale == 0)
        {
            if (!_isStop)
            {
                _currentVelocity = velocity;
                velocity = Vector3.zero;
            }
            return;
        }

        if (_isStop)
        {
            velocity = _currentVelocity;
            _isStop = false;
        }
        ApplyGravity();
        if (_useDeadZone)
        {
            if (transform.position.y <= _deadZoneY)
            {
                // 파괴실행
            }

        }
    }

    private void ApplyGravity()
    {
        AddForce(_gravityDirection * _gravityAcceleration );
    }

    public void AddForce(Vector3 direction)
    {
        //velocity += direction;
        _rigid.AddForce(direction, ForceMode.Impulse);
    }
}
