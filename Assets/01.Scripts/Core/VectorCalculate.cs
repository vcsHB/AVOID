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
}