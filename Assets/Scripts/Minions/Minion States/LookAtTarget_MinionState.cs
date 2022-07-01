using UnityEngine;

public class LookAtTarget_MinionState : StateMachineBehaviour
{
    private Minion _minion;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_minion == null) _minion = animator.gameObject.GetComponent<Minion>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_minion._attackedTarget == null) return;

        // Поворот миньона в сторону врага
        Vector3 lookDirection = (_minion._attackedTarget.transform.position - _minion.transform.position).normalized;
        if (lookDirection.x < 0 && !_minion.mecanim.initialFlipX) _minion.mecanim.initialFlipX = true;
        if (lookDirection.x >= 0 && _minion.mecanim.initialFlipX) _minion.mecanim.initialFlipX = false;
        _minion.mecanim.Initialize(true);

    }

    


}
