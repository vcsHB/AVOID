using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LogicObject))]
public abstract class InteractObject : FieldObject
{
    [SerializeField] protected float _interactCoolTime;
    public bool isActive;
    public bool canInteract = true;    
    protected float _currentTime = 0;
    [SerializeField] protected LayerMask _detectLayer;
    [SerializeField] protected float _detectRadius = 1.5f;
    protected Collider _collider;
    private LogicObject _logicObject;
    public UnityEvent interactEvent;


    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<Collider>();
    }
    

    protected virtual void Update()
    {
        if (isActive || TimeManager.TimeScale == 0)
        {
            return;
        }

        if (!canInteract)
        {
            _currentTime += TimeManager.TimeScale * Time.deltaTime;
            if (_currentTime >= _interactCoolTime)
            {
                _currentTime = 0;
                canInteract = true;
            }

            return;
        }
        
        //DetectTarget();
    }

    protected virtual void DetectTarget()
    {
        Collider[] detect = new Collider[1];
        int detectAmount = Physics.OverlapSphereNonAlloc(transform.position, _detectRadius, detect, _detectLayer);
        
        if (detectAmount == 0)
        {
            return;
        }

        IInteractable interactable = detect[0].GetComponent<IInteractable>();
        canInteract = false;
        Interact(interactable);
        
    }


    public void Interact(IInteractable interactable)
    {
        interactEvent?.Invoke();
        HandlerInteraction(interactable);
    }

    protected abstract void HandlerInteraction(IInteractable interactable);

    public override void ResetItem()
    {
        canInteract = true;
        isActive = false;
        _currentTime = 0;
    }


}
