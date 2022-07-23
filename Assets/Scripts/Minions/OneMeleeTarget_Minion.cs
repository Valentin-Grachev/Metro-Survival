

// Миньон, наносящий урон по одной цели
public class OneMeleeTarget_Minion : Minion
{
    public override void Attack()
    {
        if (!attackedTargetIsAlive) return;

        attackedTarget.health -= damage;
    }
}
