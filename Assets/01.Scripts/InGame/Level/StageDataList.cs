
using System.Collections.Generic;

namespace StageManage
{
    public class StageDataList
    {
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
    }
}