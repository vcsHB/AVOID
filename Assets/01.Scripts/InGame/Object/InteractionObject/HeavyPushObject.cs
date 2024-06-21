using UnityEngine;

public class HeavyPushObject: PushObject
{
    protected override bool HandleInteraction(IInteractable interactable)
    {
        MoveDirection = interactable.MoveDirection;
        
        if (!DetectObstacle())
            return false;

        Move();
        return true;
    }

    public override bool DetectObstacle()
    {
        RaycastHit[] hits = new RaycastHit[1];
        _collider.enabled = false;
        int amount = Physics.BoxCastNonAlloc(transform.position, _boxCastSize, MoveDirection.normalized, hits, Quaternion.identity, 4f, _obstacleLayer | _objectLayer);
        _collider.enabled = true;
        return amount == 0;
    }
}