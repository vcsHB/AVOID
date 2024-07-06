using System.Collections;
using ObjectPooling;
using UnityEngine;

public class ObjectGenerator : LogicObject
{
    [SerializeField] private GameObject[] _objectList;
    [SerializeField] private bool _turnActiveValue = true;
    [SerializeField] private PoolingType _particlePoolingType;
    [SerializeField] private float _delay = 0f;
    private bool _isActive;
    
    private void Start()
    {
        logicSolvedEvent.AddListener(HandleGenerateObjects);
    }

    public void HandleGenerateObjects()
    {
        if (_isActive) return;
        _isActive = true;
        if (_delay != 0f)
        {
            StartCoroutine(GenerateCoroutine());
        }
        else
        {
            Generate();
        }
    }

    private IEnumerator GenerateCoroutine()
    {
        yield return new WaitForSeconds(_delay);
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < _objectList.Length; i++)
        {
            _objectList[i].SetActive(_turnActiveValue);
            EffectObject effectObject = PoolManager.Instance.Pop(_particlePoolingType) as EffectObject;
            effectObject.Initialize(_objectList[i].transform.position);
            effectObject.Play();
        }
    }
}
