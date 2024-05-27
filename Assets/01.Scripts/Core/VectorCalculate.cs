using System;
using UnityEngine;

public class VectorCalculate
{
    public static Vector3 ParseIntVector(Vector3 vec)
    {
        return new Vector3(
            Mathf.FloorToInt(vec.x),
            Mathf.FloorToInt(vec.y),
            Mathf.FloorToInt(vec.z)
            );
    }

    public static Vector3 ParseMultipleVector(Vector3 vec, float count)
    {
        return new Vector3(
            vec.x / count,
            vec.y / count,
            vec.z / count
        );
    }

    public static Vector3 CalcSafeVector(Vector3 vec1, Vector3 vec2)
    {
        return new Vector3(
            vec1.x + vec2.x,
            vec1.y + vec2.y,
            vec1.z + vec2.z
        );
    }

    // public static Vector3 ClampVectorCell()
    // {
    //     
    // }

    public static Vector3 GetMoveDirection(PlayerInputDirection inputDirection, LocalDirection localDirection)
    {
        switch (localDirection)
        {
            case LocalDirection.Default:
                switch (inputDirection)
                {
                    case PlayerInputDirection.LeftUp:
                        return Vector3.forward; 
                    case PlayerInputDirection.RightUp:
                        return Vector3.right;
                    case PlayerInputDirection.LeftDown:
                        return Vector3.left;
                    case PlayerInputDirection.RightDown:
                        return Vector3.back;
                }
                break;
            case LocalDirection.Left:
                switch (inputDirection)
                {
                    case PlayerInputDirection.LeftUp:
                        return Vector3.right; 
                    case PlayerInputDirection.RightUp:
                        return Vector3.down;
                    case PlayerInputDirection.LeftDown:
                        return Vector3.up;
                    case PlayerInputDirection.RightDown:
                        return Vector3.left;
                }
                break;
            case LocalDirection.Right:
                switch (inputDirection)
                {
                    case PlayerInputDirection.LeftUp:
                        return Vector3.down; 
                    case PlayerInputDirection.RightUp:
                        return Vector3.forward;
                    case PlayerInputDirection.LeftDown:
                        return Vector3.back;
                    case PlayerInputDirection.RightDown:
                        return Vector3.up;
                }
                break;
            
        }

        return Vector3.zero;
    }
}