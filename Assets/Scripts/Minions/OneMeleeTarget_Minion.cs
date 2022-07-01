

// Миньон, наносящий урон по одной цели
public class OneMeleeTarget_Minion : Minion
{
    public override void Attack()
    {
        if (_attackedTarget == null) return;

        _attackedTarget.health -= damage;
    }
}
