using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextDisplay : LogicObject
{
    private TextMeshPro _tmp;

    [Header("Settings")] 
    [SerializeField] private float _showDuration = 1f;
    [SerializeField] private Color _color;
    private Color _offColor;
    
    private void Awake()
    {
        _tmp = transform.Find("Text").GetComponent<TextMeshPro>();
        _offColor = _color;
        _offColor.a = 0;
    }

    private void Start()
    {
        logicSolvedEvent.AddListener(ShowTextDisplay);
    }

    public void ShowTextDisplay()
    {
        _tmp.color = _offColor;
        _tmp.DOColor(_color, _showDuration);
    }

    
    
}
