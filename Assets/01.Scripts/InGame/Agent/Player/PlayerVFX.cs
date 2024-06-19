using TMPro;
using UnityEngine;

public class PlayerVFX : AgentVFX
{


    [SerializeField] private ParticleSystem _footStep;

    [SerializeField] private MeshRenderer _shieldMesh;
    [SerializeField] private ParticleSystem _collectParticle;
    [SerializeField] private TextMeshPro _moveCountText;
    
    [ContextMenu("DebugSetShieldOn")]
    public void DebugSetOnShield()
    {
        SetShield(true);
    }
    
    [ContextMenu("DebugSetShieldOff")]
    public void DebugSetOffShield()
    {
        SetShield(false);
    }
    
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

    public void SetMoveCountPanel(bool value, int count = 0)
    {
        _moveCountText.enabled = value;
        _moveCountText.text = count.ToString();
    }
    
    

    

}