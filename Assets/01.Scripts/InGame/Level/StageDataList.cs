
using System.Collections.Generic;

namespace StageManage
{
    public class StageDataList
    {
        public int currentPlayedStageId;
        public List<StageData> stageDataList;
        
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
                data = new StageData();
                stageDataList.Add(data);
            }

            data.isCleared = true;
        }
    }
}