using System;
using ObjectPooling;

public abstract class FieldObject : PoolableMono
{
    public Health HealthCompo { get; protected set; }
    
    public bool blockMovement;

    protected virtual void Awake()
    {
        HealthCompo = GetComponent<Health>();
        
        
    }

   
}