
public class OneMeleeTarget_NavMeshMinion : NavMeshMinion
{
    public override void Attack()
    {
        if (_attackedTarget == null) return;

        _attackedTarget.health -= damage;
    }
}
