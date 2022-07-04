using UnityEngine;

public class AttackWithBackDistract_MinionState : StateMachineBehaviour
{
    private Minion _minion;
    private Vector2 _moveDirection;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_minion == null) _minion = animator.gameObject.GetComponent<Minion>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_minion.attackedTarget == null) return;

        // Поворот миньона в сторону врага
        _moveDirection = (_minion.attackedTarget.transform.position - _minion.transform.position).normalized;
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

        // При атаке стоит на месте
        _minion.rigidbody.velocity = Vector3.zero;


    }

    


}
