using System.Collections.Generic;
using UnityEngine;

public class LogicObject : MonoBehaviour
{
    public Logic[] logics;
    public LogicEvent[] logicEvents;
    
    public bool SolveLogic
    {
        get
        {
            foreach (Logic logic in logics)
            {
                if (!logic.isActive)
                {
                    return false;
                }
            }

            return true;
        }
        private set { }
    }

    private Logic FindLogic(LogicType type)
    {
        for (int i = 0; i < logics.Length; i++)
        {
            if (!logics[i].isActive|| logics[i].logicType == type)
            {
                return logics[i];
            }
        }
        Debug.LogWarning("Logic is not exist : nullref");
        return null;
    }

    public void TriggerLogic(LogicType logicType)
    {
        Logic logic = FindLogic(logicType);
        logic.Trigger();
        
        if (SolveLogic)
        {
            for (int i = 0; i < logicEvents.Length; i++)
            {
                logicEvents[i].SolveLogic();

            }
        }
    }
}