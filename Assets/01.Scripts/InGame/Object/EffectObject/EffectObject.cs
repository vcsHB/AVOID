using System.Collections;
using UnityEngine;

public class EffectObject : MonoBehaviour
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
}