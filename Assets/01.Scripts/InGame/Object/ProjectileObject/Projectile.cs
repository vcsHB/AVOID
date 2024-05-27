using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float _speed = 4f;
    [SerializeField] protected float _lifeTime = 10f;
    [SerializeField] protected Transform _visualTrm;
    
    protected Vector3 _direction;

    private Rigidbody _rigid;


    protected virtual void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }


    [ContextMenu("DebugShoot")]
    private void DebugShoot()
    {
        Fire(new Vector3(0,1,1));
    }
    
    public void Fire(Vector3 direction)
    {
        _direction = direction;
        transform.forward = _direction;
        _rigid.velocity = _direction * _speed;
    }
    

}