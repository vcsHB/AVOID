
using System.Collections.Generic;
using UnityEngine;

namespace StageManage
{
    public class StageDataList
    {
        public int currentPlayedStageId;
        public List<StageData> stageDataList = new List<StageData>();
        
        public StageData FindStage(int id)
        {
            for (int i = 0; i < stageDataList.Count; i++)
            {
                if (stageDataList[i].id == id)
                {
                    return stageDataList[i];
                }    
            }

            return null;
        }

        public bool CheckClear(int id)
        {
            StageData data = FindStage(id);
            if (data == null) return false;
            
            return data.isCleared;
        }

        public void Clear(int id)
        {
            StageData data = FindStage(id);
            if (data == null)
            {
                Debug.Log($"id {id}의 Data가 존재하지 않음 -> 새로만듬");
                data = new StageData(id);
                stageDataList.Add(data);
            }

            data.isCleared = true;
        }
    }
}