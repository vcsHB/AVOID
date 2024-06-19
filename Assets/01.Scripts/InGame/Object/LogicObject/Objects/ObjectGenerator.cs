using ObjectPooling;
using UnityEngine;

public class ObjectGenerator : LogicObject
{
    [SerializeField] private GameObject[] _objectList;
    [SerializeField] private bool _turnActiveValue = true;
    [SerializeField] private PoolingType _particlePoolingType;
    
    private void Start()
    {
        logicSolvedEvent.AddListener(HandleGenerateObjects);
    }

    public void HandleGenerateObjects()
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
