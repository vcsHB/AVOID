using System;
using SoundManage;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SoundObject))]
public class LogicObject : FieldObject
{
    public Logic[] logics;
    public UnityEvent logicSolvedEvent;
    public SoundObject soundCompo { get; protected set; }

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


    protected virtual void Awake()
    {
        soundCompo = GetComponent<SoundObject>();
        for (int i = 0; i < logics.Length; i++)
        {
            logics[i] = Instantiate(logics[i]);
        }
    }


    public void TriggerLogic(int logicIndex, bool triggerValue)
    {
        if (logicIndex >= logics.Length) return;
        
        logics[logicIndex].SetActive(triggerValue);

        CheckSolved();
    }

    protected void CheckSolved()
    {
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


    public override void ResetItem()
    {
        throw new NotImplementedException();
    }
}