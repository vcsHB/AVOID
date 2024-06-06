using TMPro;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    private TextMeshPro _tmp;

    [Header("Settings")] [SerializeField] private int a;
    
    private void Awake()
    {
        _tmp = transform.Find("Text").GetComponent<TextMeshPro>();
    }
    
    
}
