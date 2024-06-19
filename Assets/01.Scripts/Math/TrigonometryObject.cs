using System.Collections.Generic;
using Math;
using UnityEngine;

public class TrigonometryObject : MonoBehaviour
{
    [SerializeField] private float _waveSpeed;
    [SerializeField] private float _waveWidth;
    [SerializeField] private float _rotateBy; 
    [SerializeField] private Vector3 _offset;
    [SerializeField] private bool _useSpiralRotate;
    [SerializeField] private float _sprialRotateSpeed = 1.5f;
    [SerializeField] private float _currentTime = 0;
    [SerializeField]  private float _timeOffset;
    [SerializeField] private List<Trigonometry> _triFunction;
    
    private void Update()
    {
        if (_useSpiralRotate)
            _currentTime += Time.deltaTime * _sprialRotateSpeed;
        transform.localPosition = 
            (_waveWidth * Wave()) * new Vector3(
                Mathf.Cos(_timeOffset+_currentTime),
                Mathf.Sin(_timeOffset+_currentTime),
                0) + _offset;
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