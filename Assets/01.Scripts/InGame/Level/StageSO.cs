using UnityEngine;

namespace StageManage
{
    [CreateAssetMenu(menuName = "SO/Stage/Stage")]
    public class StageSO : ScriptableObject
    {
        public int id;
        public string stageName;
        public Sprite stageIcon;
        public Level levelPrefab;
    
    }
    
}