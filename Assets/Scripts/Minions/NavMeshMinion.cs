using UnityEngine;
using UnityEngine.AI;

public abstract class NavMeshMinion : Minion
{
    protected NavMeshAgent _agent; public NavMeshAgent agent { get => _agent; }
    public override Transform destination 
    { 
        get => base.destination;
        set
        {
            base.destination = value;
            _agent.SetDestination(value.position);
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

    protected override void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        base.Start();
    }


}
