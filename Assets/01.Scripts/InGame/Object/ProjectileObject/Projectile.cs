using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float _speed = 4f;
    [SerializeField] protected float _lifeTime = 10f;
    [SerializeField] protected Transform _visualTrm;
    
    protected Vector3 _direction;

    public void Fire(Vector3 direction)
    {
        _direction = direction;
           
        
        
    }
    

}