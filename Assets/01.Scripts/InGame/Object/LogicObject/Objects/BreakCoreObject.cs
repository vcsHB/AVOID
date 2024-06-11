using TMPro;
using UnityEngine;

public class BreakCoreObject : LogicObject, IDamageable
{
    [SerializeField] private MeshRenderer _energyBendMeshRenderer;
    [SerializeField] private int _shieldAmount = 2;

    [SerializeField] private ParticleSystem _explodeParticle;
    private int _shieldMaterialHash;
    [SerializeField] private TextMeshPro _tmp;

    private void Awake()
    {
        _shieldMaterialHash = Shader.PropertyToID("_Amount");
        
    }

    

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        SetShieldAmount();
    }
    

    public void DecreaseShield()
    {
        if (_shieldAmount == 0) return;
        _explodeParticle.Play();

        _shieldAmount--;
        SetShieldAmount();
    }
    
   
    private void SetShieldAmount()
    {
        if (_shieldAmount <= 0)
        {
            // 실드 다뿌숨
            _energyBendMeshRenderer.enabled = false;
            logicSolvedEvent?.Invoke();
            _tmp.color = Color.white;
        }
        else
        {
            _energyBendMeshRenderer.enabled = true;
            _tmp.color = Color.magenta;
        }

        _tmp.text = _shieldAmount.ToString();
        _energyBendMeshRenderer.material.SetInt(_shieldMaterialHash, _shieldAmount);
        
    }

    public void TakeDamage(int damage)
    {
        DecreaseShield();
    }

    public void RestoreHealth(int amount)
    {
        _shieldAmount += amount;
        SetShieldAmount();
    }

    public void Die()
    {
        
    }
}
