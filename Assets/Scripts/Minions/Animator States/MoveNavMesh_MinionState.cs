using UnityEngine;

public class MoveNavMesh_MinionState : StateMachineBehaviour
{
    private NavMeshMinion _minion;
    private Vector2 _moveDirection;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_minion == null) _minion = animator.gameObject.GetComponent<NavMeshMinion>();
        _minion.moveSpeed = _minion.moveSpeed;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // Поворот в сторону атаки
        _moveDirection = _minion.agent.velocity;

        if (_moveDirection.x < 0 && !_minion.mecanim.initialFlipX)
        {
            _minion.mecanim.initialFlipX = true;
            _minion.mecanim.Initialize(true);
        }
        
        if (_moveDirection.x >= 0 && _minion.mecanim.initialFlipX)
        {
            _minion.mecanim.initialFlipX = false;
            _minion.mecanim.Initialize(true);
        }
        
        // Обновление места назначения
        if (_minion.destination != null) _minion.agent.SetDestination(_minion.destination.position);


        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _minion.agent.speed = 0f;
        _minion.agent.velocity = Vector3.zero;
    }


}
