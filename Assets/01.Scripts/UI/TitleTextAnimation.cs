using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TitleTextAnimation : MonoBehaviour
{
    private TMP_Text _tmpText;

    [Header("TextAnimation Setting")] 
    [SerializeField] private float _textMoveSpeed;

    [SerializeField] private float _startTerm = 3f;
    [SerializeField] private float _textDefaultYDelta = 300f;
    [SerializeField] private float _textTimeStep = 0.1f;
    private float _currentTextYDelta = 0;
    
    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        StartCoroutine(TitleStartCoroutine());
    }

    private IEnumerator TitleStartCoroutine()
    {
        yield return new WaitForSeconds(_startTerm);
        _tmpText.enabled = true;

        Play();
    }

    [ContextMenu("Play")]
    public void Play()
    {
        StartCoroutine(TextAnimationCoroutine());
    }

    private IEnumerator TextAnimationCoroutine()
    {
        float currentTime = -2f;
        while (true)
        {
            currentTime += Time.deltaTime * _textMoveSpeed;
            //_currentTextYDelta = Mathf.Lerp(_textDefaultYDelta, 0, ratio);
            
            
            // 입력된 텍스트 스트링으로 메시 정보를 만들어주는거
            _tmpText.ForceMeshUpdate();

            TMP_TextInfo textInfo = _tmpText.textInfo; // 해당 메시에 들어가있는 텍스트 정보를 가져옴

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            
                if(charInfo.isVisible == false)continue;

                Vector3[] vertices = textInfo.meshInfo[0].vertices;

                int v0 = charInfo.vertexIndex;

                float time = Mathf.Clamp01(currentTime - _textTimeStep * i);

                Vector3 offset = Vector3.Lerp(new Vector3(0, _textDefaultYDelta), Vector3.zero, time); 

                for (int j = 0; j < 4; j++)
                {
                    Vector3 origin = vertices[v0 + j];
                    vertices[v0 + j] = origin + offset;
                }
            }
            _tmpText.UpdateVertexData();
            if(currentTime - _textTimeStep * textInfo.characterCount-1 >= 1)
                yield break;
            
            yield return null;
        }
    }
    
}
