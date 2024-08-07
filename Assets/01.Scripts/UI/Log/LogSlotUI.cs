using TMPro;
using UnityEngine;

public class LogSlotUI : MonoBehaviour
{
    [SerializeField] private StageData _stageData;
    [SerializeField] private TextMeshProUGUI _stageNumberingText;
    [SerializeField] private TextMeshProUGUI _moveAmountText;

    public float height;
    public RectTransform rectTrm;
    
    public void Initialize(StageData data)
    {
        _stageData = data;
        _stageNumberingText.text = $"stage {_stageData.id}";
        _moveAmountText.text = $"{_stageData.moveCount} Moves";
        rectTrm = transform as RectTransform;
        height = rectTrm.rect.size.y;
    }
}
