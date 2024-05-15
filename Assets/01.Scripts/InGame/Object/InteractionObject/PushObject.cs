using System.Collections;
using UnityEngine;

public class PushObject : InteractObject
{
    private float _moveTime = 1.2f;
    private Vector3 _direction;
    private Rigidbody _rigid;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    public override void Interact(Agent agent)
    {
        _direction = agent.moveDirection;
        Move();

    }

    private void Move()
    {
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        float currentTime = 0;
        Vector3 beforePosition = transform.position;
        Vector3 targetPosition = _direction + beforePosition;
        while (currentTime <= _moveTime)
        {
            _rigid.MovePosition(
                    Vector3.Lerp(
                            beforePosition,
                            targetPosition,
                            currentTime/_moveTime
                        )    
                );
            yield return null;
        }
        transform.position = targetPosition;
    }
}