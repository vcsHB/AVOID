using UnityEngine;

namespace StageManage
{
    [CreateAssetMenu(menuName = "SO/Stage/StageList")]
    public class StageListSO : ScriptableObject
    {
        public StageSO[] stageList;

        public StageSO FindStage(int id)
        {
            for (int i = 0; i < stageList.Length; i++)
            {
                if (stageList[i].id == id)
                {
                    return stageList[i];
                }    
            }

            return null;
        }
    
    }
}