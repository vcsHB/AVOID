
using System;
using UnityEngine;

namespace Math
{
    
    public enum TrigonometryFunctions
    {
        Sin,
        Cos,
        Tan
    }
    [System.Serializable]
    public struct Trigonometry
    {
        public float height;
        public TrigonometryFunctions func;
        public float time;
        
        public float Value(float x)
        {
            switch (func)
            {
                case TrigonometryFunctions.Sin:
                    return height * Mathf.Sin(time * x);
                case TrigonometryFunctions.Cos:
                    return height * Mathf.Cos(time * x);
                case TrigonometryFunctions.Tan:
                    return height * Mathf.Tan(time * x);
            }

            return 0f;
        }
    }
}