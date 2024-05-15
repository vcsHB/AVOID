using UnityEngine;

public abstract class InteractObject : MonoBehaviour
{
    [SerializeField] protected float _interactCoolTime;
    public bool isActive;
    public bool canInteract = true;    
    protected float _currentTime = 0;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _detectRadius = 1.5f;
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (isActive || !canInteract) return;
        if (other.TryGetComponent(out Agent agent))
        {
            Interact(agent);
        }
        
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
        
        DetectPlayer();
    }

    protected void DetectPlayer()
    {
        Collider[] detect = new Collider[1];
        int detectAmount = Physics.OverlapSphereNonAlloc(transform.position, _detectRadius, detect, _playerLayer);

        if (detectAmount == 0)
        {
            return;
        }

        Agent agent = detect[0].GetComponent<Agent>();
        canInteract = false;
        Interact(agent);
        
    }


    public abstract void Interact(Agent agent);




}
