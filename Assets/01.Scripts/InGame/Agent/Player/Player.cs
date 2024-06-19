using System.Collections;
using UnityEngine;

public class Player : Agent
{
    public PlayerVFX PlayerVFXCompo { get; protected set; }
    public PlayerController PlayerMovementCompo { get; protected set; }
    private MeshRenderer _visualRenderer;
    private int _playerDefaultLayer, _deadBodyLayer;
    private int _playerDissolveHash;
    [SerializeField] private bool _isReviveShield;

    protected override void Awake()
    {
        base.Awake();
        PlayerVFXCompo = VFXCompo as PlayerVFX;
        PlayerMovementCompo = MovementCompo  as PlayerController;
        _visualRenderer = transform.Find("Visual").GetComponent<MeshRenderer>();
        _playerDissolveHash = Shader.PropertyToID("_DissolveHeight");
        _playerDefaultLayer = LayerMask.NameToLayer("Player");        
        _deadBodyLayer = LayerMask.NameToLayer("DeadBody");        
    }

    protected override void Start()
    {
        HealthCompo.OnDieEvent += HandleAgentDie;
    }

    public override void HandleAgentDie()
    {
        base.HandleAgentDie();
        CameraManager.Instance.Shake(10f,0.15f);
        MovementCompo.SetStun(true);
        PlayerVFXCompo.UpdateFootStep(false);
        gameObject.layer = _deadBodyLayer;
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        yield return DissolveCoroutine();
        yield return new WaitForSeconds(1f);
        Revive();
    }
    private IEnumerator DissolveCoroutine()
    {
        float currentTime = 0;
        while (currentTime < 1)
        {
            currentTime += Time.deltaTime;
            _visualRenderer.material.SetFloat(_playerDissolveHash, Mathf.Lerp(2, -2, currentTime));
            yield return null;
        }
        _visualRenderer.material.SetFloat(_playerDissolveHash, -2);
    }

    

    [ContextMenu("Revive")]
    public void Revive()
    {
        // 부활하는 코드
        PlayerVFXCompo.UpdateFootStep(true);
        HealthCompo.Initialize(this);
        StartCoroutine(ReviveCoroutine());
        
    }

    private IEnumerator ReviveCoroutine()
    {
        yield return LevelManager.Instance.ResetLevel();
        MovementCompo.SetDefaultRotate();

        _visualRenderer.material.SetFloat(_playerDissolveHash, 2);
        gameObject.layer = _playerDefaultLayer;
        if(_isReviveShield)
            PlayerSkillManager.Instance.GetSkill(PlayerSkillEnum.Shield).UseSkill();
        PlayerSkillManager.Instance.GetSkill<PlayerMoveCountSkill>().DisableSkill();
        MovementCompo.SetStun(false);

    }

    private void HandleHit()
    {
        
    }
}