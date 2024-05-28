using System;
using System.Collections.Generic;
using UnityEngine;

public class LogicObject : MonoBehaviour
{
    public Logic[] logics;
    public event Action logicSolvedEvent;

    private bool _isSolvedLogic;
    public bool IsSolvedLogic => _isSolvedLogic;

    private Logic FindLogic(LogicType type)
    {
        for (int i = 0; i < logics.Length; i++)
        {
            if (!logics[i].isActive && logics[i].logicType == type)
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

        if (_isSolvedLogic) return;
        for (int i = 0; i < logics.Length; i++)
        {
            if (!logics[i].isActive)
            {
                return;
            }
        }
        logicSolvedEvent?.Invoke();
    }
}