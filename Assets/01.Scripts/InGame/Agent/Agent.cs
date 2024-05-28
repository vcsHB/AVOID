using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public abstract class Agent : MonoBehaviour, IInteractable
{
    #region Component

    public AgentMovement MovementCompo { get; protected set; }
    public Rigidbody rigid { get; protected set; }


    #endregion


    public Vector3 MoveDirection { get; set; }
    public void DetectInteraction()
    {
        throw new NotImplementedException();
    }
}