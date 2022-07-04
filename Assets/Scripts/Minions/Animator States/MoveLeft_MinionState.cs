using UnityEngine;

public class MoveLeft_MinionState : StateMachineBehaviour
{
    private Minion _minion;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_minion == null) _minion = animator.gameObject.GetComponent<Minion>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_minion.destination == null) return;
        _minion.rigidbody.velocity = Vector2.left * _minion.moveSpeed;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _minion.rigidbody.velocity = Vector3.zero;
    }


}
