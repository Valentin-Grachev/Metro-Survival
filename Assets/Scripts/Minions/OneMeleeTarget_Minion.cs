

// ������, ��������� ���� �� ����� ����
public class OneMeleeTarget_Minion : Minion
{
    public override void Attack()
    {
        if (attackedTarget == null) return;

        attackedTarget.health -= damage;
    }
}
