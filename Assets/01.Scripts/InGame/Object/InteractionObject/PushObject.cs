using System.Collections;
using UnityEngine;

public class PushObject : InteractObject, IInteractable
{
    [SerializeField]
    private float _moveTime = 0.7f;
    private Rigidbody _rigid;
    [SerializeField]
    private LayerMask _objectLayer;

    private Vector3 _boxCastSize = Vector3.one * 0.5f;
    [SerializeField] private LayerMask _obstacleLayer;
    public Vector3 MoveDirection { get; set; }


    
    protected override void Awake()
    {
        base.Awake();
        _rigid = GetComponent<Rigidbody>();
    }

    protected override bool HandlerInteraction(IInteractable interactable)
    {
        MoveDirection = interactable.MoveDirection;

        if (!DetectObstacle())
            return false;

        _collider.enabled = false;
        if (!DetectInteraction())
        {
            _collider.enabled = true;
            return false;

        }
        _collider.enabled = true;
        
        Move();
        return true;
    }

    private void Move()
    {
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        float currentTime = 0;
        Vector3 beforePosition = transform.position;
        Vector3 targetPosition = beforePosition + MoveDirection;
        while (currentTime < _moveTime)
        {
            currentTime += Time.deltaTime;
            transform.position =
                Vector3.Lerp(
                    beforePosition,
                    targetPosition,
                    Mathf.Clamp01(currentTime / _moveTime)
                );
            yield return null;
        }
        transform.position = targetPosition;
    }

    public bool DetectInteraction()
    {
        RaycastHit[] hits = new RaycastHit[5];
        int amount = Physics.BoxCastNonAlloc(transform.position, _boxCastSize, MoveDirection.normalized, hits, Quaternion.identity, 4f, _objectLayer);
        if (amount == 0) return true;

        for (int i = 0; i < amount; i++)
        {
            if (hits[i].transform.TryGetComponent(out InteractObject interactObject))
            {
                if (!interactObject.Interact(this)) return false;
            }
        }

        return true;
    }
    
    public bool DetectObstacle()
    {
        RaycastHit[] hits = new RaycastHit[1];
        int amount = Physics.BoxCastNonAlloc(transform.position, _boxCastSize, MoveDirection.normalized, hits, Quaternion.identity, 4f, _obstacleLayer);
        return amount == 0;
    }
    
    


}