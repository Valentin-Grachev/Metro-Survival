using UnityEngine;
using UnityEngine.AI;

public abstract class NavMeshMinion : Minion
{
    [SerializeField] protected Vector2 _detectionArea;
    protected NavMeshAgent _agent; public NavMeshAgent agent { get => _agent; }
    


    public override float moveSpeed
    { 
        get => base.moveSpeed;
        set
        {
            base.moveSpeed = value;
            if (_agent == null)
            {
                _agent = GetComponent<NavMeshAgent>();
                _agent.updateRotation = false;
                _agent.updateUpAxis = false;
            }
            
            _agent.speed = value;
        }
    }


    protected override void Run()
    {
        base.Run();
        if (isDeath) return;

        // Всегда ищем ближайшую цель
        
        Team detectionTeam;
        if (team == Team.Ally) detectionTeam = Team.Enemy;
        else detectionTeam = Team.Ally;

        if (Library.TryFindNearestInsideArea(transform.position, _detectionArea, detectionTeam, out DestroyableObject found))
            destination = found;


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
        agent.isStopped = false;
        agent.speed = moveSpeed;

        // Поворот в сторону атаки
        Vector2 moveDirection = agent.velocity;

        if (moveDirection.x < 0 && _currentIsRightSide)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _currentIsRightSide = false;
        }

        else if (moveDirection.x > 0 && !_currentIsRightSide)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _currentIsRightSide = true;
        }

        // Обновление места назначения
        if (destinationIsAlive) agent.SetDestination(destination.transform.position);
    }

    public void StopNavMesh_ExitState()
    {
        agent.speed = 0f;
        agent.isStopped = true;
        agent.velocity = Vector2.zero;
    }



    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, _detectionArea);
    }



}
