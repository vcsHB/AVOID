using System;
using UnityEngine;

public struct PlatformInfo
{
    public LocalDirection localDirection;

    public Vector3 NormalDirection
    {
        get
        {
            switch (localDirection)
            {
                case LocalDirection.Default:
                    return Vector3.up;
                case LocalDirection.Left:
                    return Vector3.back;
                case LocalDirection.Right:
                    return Vector3.left;
            }

            return Vector3.zero;
        }
    }
}