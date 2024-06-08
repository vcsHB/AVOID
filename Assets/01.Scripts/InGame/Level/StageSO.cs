using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stage")]
public class StageSO : ScriptableObject
{
    public int id;
    public string stageName;
    public Level levelPrefab;
    
}