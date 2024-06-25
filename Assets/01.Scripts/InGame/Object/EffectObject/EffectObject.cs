using System.Collections;
using ObjectPooling;
using UnityEngine;

public class EffectObject : PoolableMono
{
    [SerializeField] private bool _isOnEnablePlay;
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private bool _isLoop;
    [SerializeField] private float _lifeTime = 1f;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (_isOnEnablePlay)
        {
            Play();
        }
    }

    public void Initialize(Vector3 position)
    {
        SetPosition(position);
    }
    
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Play()
    {
        _audioSource.Play();
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Play();
        }

        if (_isLoop) return;
        StartCoroutine(PlayCoroutine());
    }

    public void PlayWithoutSFX()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Play();
        }

        if (_isLoop) return;
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy();
    }

    private void Destroy()
    {
        PoolManager.Instance.Push(this);
    }

    public override void ResetItem()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Stop();
        }
    }
}