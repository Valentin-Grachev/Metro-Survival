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

        // ������ ���� ��������� ����
        
        Team detectionTeam;
        if (team == Team.Ally) detectionTeam = Team.Enemy;
        else detectionTeam = Team.Ally;

        if (Library.TryFindNearestInsideArea(transform.position, _detectionArea, detectionTeam, out DestroyableObject found))
            destination = found;

        

        //if (!destinationIsAlive) spineAnimation.SetAnimation(AnimationType.Idle);


    }

    protected override void Start()
    {
        base.Start();
        if (_agent == null) _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }


    // ====== �������-��������� ======

    public void MoveNavMesh_UpdateState()
    {
        agent.isStopped = false;
        agent.speed = moveSpeed;

        // ������� � ������� �����
        Vector2 moveDirection = agent.velocity;

        if (moveDirection.x < 0 && transform.localScale.x > 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        else if (moveDirection.x > 0 && transform.localScale.x < 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        // ���������� ����� ����������
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
