using UnityEngine.AI;

public abstract class NavMeshMinion : Minion
{
    protected NavMeshAgent _agent; public NavMeshAgent agent { get => _agent; }

    public override float moveSpeed
    { 
        get => base.moveSpeed;
        set
        {
            base.moveSpeed = value;
            _agent.speed = value;
        }
    }

    protected override void OnEnable()
    {
        if (_agent == null) _agent = GetComponent<NavMeshAgent>();

        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();

        // ≈сли точка назначени€ уничтожена - обнул€ем ее
        if (destination != null && !destination.gameObject.activeInHierarchy) destination = null;
    }




    protected override void Start()
    {
        if (_agent == null) _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        base.Start();
    }


}
