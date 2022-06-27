using UnityEngine;

public class MoveNavMesh_MinionState : StateMachineBehaviour
{
    private NavMeshMinion _minion;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_minion == null) _minion = animator.gameObject.GetComponent<NavMeshMinion>();
        _minion.moveSpeed = _minion.moveSpeed;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_minion.destination == null) return;
        Vector3 moveDirection = (_minion.destination.position - _minion.transform.position).normalized;
        if (moveDirection.x < 0 && !_minion.mecanim.initialFlipX) _minion.mecanim.initialFlipX = true;
        if (moveDirection.x >= 0 && _minion.mecanim.initialFlipX) _minion.mecanim.initialFlipX = false;
        _minion.mecanim.Initialize(true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _minion.agent.speed = 0f;
    }


}
