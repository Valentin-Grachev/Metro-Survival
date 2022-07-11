using UnityEngine;
using UnityEngine.AI;

public abstract class NavMeshMinion : Minion
{
    protected NavMeshAgent _agent; public NavMeshAgent agent { get => _agent; }
    protected DestroyableObject _destinationObject;


    public override Transform destination
    { 
        get => base.destination;
        set
        {
            base.destination = value;
            if (value != null) _destinationObject = value.GetComponent<DestroyableObject>();
        }
    }

    public override float moveSpeed
    { 
        get => base.moveSpeed;
        set
        {
            base.moveSpeed = value;
            _agent.speed = value;
        }
    }

    protected override void OnEnabled()
    {
        if (_agent == null) _agent = GetComponent<NavMeshAgent>();

        base.OnEnabled();
    }

    protected override void Run()
    {
        base.Run();

        // ≈сли точка назначени€ уничтожена - обнул€ем ее
        if (destination != null && _destinationObject.isDeath) destination = null;
    }




    protected override void Start()
    {
        if (_agent == null) _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        base.Start();
    }


}
