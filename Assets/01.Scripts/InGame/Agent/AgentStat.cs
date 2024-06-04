using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat")]
public class AgentStat : ScriptableObject
{
    public int atk;
    public int Health;
    public bool IsResist;
    public float MoveCooltime;
    
}