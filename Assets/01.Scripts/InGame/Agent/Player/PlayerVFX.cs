using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFX : AgentVFX
{


    [SerializeField] private ParticleSystem _footStep;

    [SerializeField] private MeshRenderer _shieldMesh;
    [SerializeField] private ParticleSystem _collectParticle;

    public void PlayCollectParticle()
    {
        _collectParticle.Play();
    }
    
    

    public void UpdateFootStep(bool value)
    {
        if (value)
        {
            _footStep.Play();
        }
        else
        {
            _footStep.Stop();
        }
    }

    public void SetShield(bool value)
    {
        _shieldMesh.enabled = value;
    }

    

}