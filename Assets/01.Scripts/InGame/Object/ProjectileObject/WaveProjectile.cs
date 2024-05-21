using System.Collections.Generic;
using Math;
using UnityEngine;

public class WaveProjectile : Projectile, ITriMovement
{
    [SerializeField] private float _waveSpeed;
    [SerializeField] private float _waveWidth;
    [SerializeField] private float _rotateBy;
    
    [SerializeField] private List<Trigonometry> _triFunction;

    public void LoopMove(Trigonometry triFunc)
    {
        //_visualTrm.localPosition = 
    }
}