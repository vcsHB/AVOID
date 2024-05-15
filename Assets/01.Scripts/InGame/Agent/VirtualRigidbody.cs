using UnityEngine;

public class VirtualRigidbody : MonoBehaviour
{
    [SerializeField] private Vector3 _gravityDirection;
    [SerializeField] private float _gravityAcceleration = 9.8f;
    
    private Rigidbody _rigid;
    public Vector3 velocity => _rigid.velocity;
    private Vector3 _currentVelocity;
    private bool _isStop;
    
    
    private void FixedUpdate()
    {
        if (TimeManager.TimeScale == 0)
        {
            if (!_isStop)
            {
                _currentVelocity = _rigid.velocity;
                _rigid.velocity = Vector3.zero;
            }
            return;
        }

        if (_isStop)
        {
            _rigid.velocity = _currentVelocity;
            _isStop = false;
        }
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        _rigid.AddForce(_gravityDirection * _gravityAcceleration * Time.deltaTime);
    }
}
