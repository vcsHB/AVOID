using DG.Tweening;
using UnityEngine;

public class Portal : LogicObject
{
    [Header("Portal Setting")]
    [SerializeField] private int _targetStageID;
    [SerializeField]
    private bool _isPortalActivate = false;
    [SerializeField] private Transform _visualTrm;
    private float _defaultSize = 0;
    [SerializeField] private float _targetSize;
    [SerializeField] private float _activeDuration;
    private ParticleSystem _portalEffectParticle;
    private ParticleSystem _portalActiveParticle;
    private Collider _collider;

    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<Collider>();
        _portalEffectParticle = GetComponentInChildren<ParticleSystem>();
        _portalActiveParticle = _visualTrm.Find("GenerateParticle").GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        logicSolvedEvent.AddListener(HandlePortalActivate);
    }

    public void HandlePortalActivate()
    {
        _isPortalActivate = true;
        SetVisual(true);
    }

    
    [ContextMenu("SetOn")]
    private void SetOn()
    {
        SetVisual(true);
    }
    [ContextMenu("SetOff")]
    private void SetOff()
    {
        SetVisual(false);
    }
    

    private void SetVisual(bool value)
    {
        if (value)
        {
            _visualTrm.DOScale(_targetSize, _activeDuration).SetEase(Ease.OutBounce);
            _portalActiveParticle.Play();
            _portalEffectParticle.Play();
        }
        else
        {
            _visualTrm.DOScale(_defaultSize, _activeDuration).SetEase(Ease.InBounce);
            _portalEffectParticle.Stop();

        }
        _collider.enabled = value;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isPortalActivate) return;
        
        // 포탈을 탔을때 이벤트
        if (other.CompareTag("Player"))
        {
            GetPortal();
            
        }
    }

    private void GetPortal()
    {
        GameManager.Instance.stageManager.ClearStageByPortal(_targetStageID);
    }
}
