using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFX : AgentVFX
{


    [SerializeField] private VisualEffect _footStep;

    [SerializeField] private ParticleSystem _oraParticle;
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

    public void PlayStartOraParticle()
    {
        _oraParticle.Play();
    }

    public void PlayStopOraParticle()
    {
        _oraParticle.Stop();
    }

}