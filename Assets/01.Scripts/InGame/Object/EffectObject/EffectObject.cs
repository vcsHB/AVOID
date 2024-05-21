using System.Collections;
using ObjectPooling;
using UnityEngine;

public class EffectObject : PoolableMono
{
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private float _lifeTime = 1f;
    
    
    public void Play()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Play();
        }
    }

    private IEnumerator PlayCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    public override void ResetItem()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Stop();
        }
    }
}