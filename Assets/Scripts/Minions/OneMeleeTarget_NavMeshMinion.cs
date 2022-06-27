
public class OneMeleeTarget_NavMeshMinion : NavMeshMinion
{
    public override void Attack()
    {
        if (target == null) return;

        target.health -= damage;
    }
}
