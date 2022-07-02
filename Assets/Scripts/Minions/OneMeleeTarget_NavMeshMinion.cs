
public class OneMeleeTarget_NavMeshMinion : NavMeshMinion
{
    public override void Attack()
    {
        if (attackedTarget == null) return;

        attackedTarget.health -= damage;
    }
}
