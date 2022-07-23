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

        // Если точка назначения уничтожена - обнуляем ее
        if (destination != null && _destinationObject.isDeath) destination = null;
    }

    protected override void Start()
    {
        base.Start();
        if (_agent == null) _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }


    // ====== Функции-состояния ======

    public void MoveNavMesh_UpdateState()
    {
        // Поворот в сторону атаки
        Vector2 moveDirection = agent.velocity;

        if (moveDirection.x < -Constants.reversEpsilon && spineAnimation.skeletonAnimation.initialFlipX)
        {
            spineAnimation.skeletonAnimation.initialFlipX = true;
            spineAnimation.skeletonAnimation.Initialize(true);
        }

        if (moveDirection.x >= Constants.reversEpsilon && spineAnimation.skeletonAnimation.initialFlipX)
        {
            spineAnimation.skeletonAnimation.initialFlipX = false;
            spineAnimation.skeletonAnimation.Initialize(true);
        }

        // Обновление места назначения
        if (destination != null) agent.SetDestination(destination.position);
    }






}
