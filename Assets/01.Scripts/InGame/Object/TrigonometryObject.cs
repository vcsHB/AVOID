using System.Collections.Generic;
using Math;
using UnityEngine;

public class TrigonometryObject : MonoBehaviour
{
    [SerializeField] private float _waveSpeed;
    [SerializeField] private float _waveWidth;
    [SerializeField] private float _rotateBy;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _timeOffset;
    [SerializeField] private List<Trigonometry> _triFunction;

    
    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, _waveWidth * Wave(), transform.localPosition.z) + _offset;
    }

    private float Wave()
    {
        float y = 0;
        for (int i = 0; i < _triFunction.Count; i++)
        {
            y += _triFunction[i].Value(Time.time * _waveSpeed + _timeOffset);
        }
        return y;
    }
}