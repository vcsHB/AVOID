using UnityEngine;

public interface IInteractable
{
    public Vector3 MoveDirection { get; set; }

    public bool DetectInteraction();
}