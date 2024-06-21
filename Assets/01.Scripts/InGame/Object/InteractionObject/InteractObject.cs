using SoundManage;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SoundObject))]
public abstract class InteractObject : FieldObject
{
    [SerializeField] protected float _interactCoolTime;
    public bool isActive;
    public bool canInteract = true;    
    protected float _currentTime = 0;
    [SerializeField] protected LayerMask _detectLayer;
    [SerializeField] protected float _detectRadius = 1.5f;
    protected Collider _collider;
    public UnityEvent<int, bool> interactEvent;
    [SerializeField] protected int _logicIndex;
    public SoundObject SoundCompo { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<Collider>();
        SoundCompo = GetComponent<SoundObject>();
    }
    

    protected virtual void Update()
    {
        if (isActive)
        {
            return;
        }

        if (!canInteract)
        {
            _currentTime += Time.deltaTime;
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


    public virtual bool Interact(IInteractable interactable)
    {
        interactEvent?.Invoke(_logicIndex, true);
        return HandleInteraction(interactable);
    }

    protected abstract bool HandleInteraction(IInteractable interactable);

    public override void ResetItem()
    {
        canInteract = true;
        isActive = false;
        _currentTime = 0;
    }


}
