using System.Collections;
using UnityEngine;

public class PushObject : InteractObject, IInteractable
{
    [SerializeField]
    private float _moveTime = 0.7f;
    private Vector3 _direction;
    private Rigidbody _rigid;
    [SerializeField]
    private LayerMask _objectLayer;

    public Vector3 MoveDirection { get; set; }


    
    protected override void Awake()
    {
        base.Awake();
        _rigid = GetComponent<Rigidbody>();
    }

    public override void Interact(IInteractable interactable)
    {
        print("밀쳐짐");
        _direction = interactable.MoveDirection;
        MoveDirection = _direction;
        _collider.enabled = false;
        DetectInteraction();
        _collider.enabled = true;
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
            currentTime += Time.deltaTime;
            transform.position =
                Vector3.Lerp(
                    beforePosition,
                    targetPosition,
                    currentTime / _moveTime
                );
            yield return null;
        }
        transform.position = targetPosition;
    }

    public void DetectInteraction()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up, _direction, 1.5f, _objectLayer);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.TryGetComponent(out InteractObject interactObject))
            {
                interactObject.Interact(this);
            }
        }
    }
}