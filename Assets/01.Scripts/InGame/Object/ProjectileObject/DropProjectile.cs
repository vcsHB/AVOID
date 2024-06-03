using UnityEngine;

public class DropProjectile : Projectile
{
    [SerializeField] private float _dropOffset = 10f;
    public override void Fire(Vector3 firePos, Vector3 direction, int damage = 5)
    {
        //base.Fire(firePos, direction, damage);
        transform.position = firePos + Vector3.up * _dropOffset;
        _rigid.velocity = Vector3.down * _speed;
    }


    public override void ResetItem()
    {
        
    }
}