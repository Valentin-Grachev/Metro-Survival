

// ������, ��������� ���� �� ����� ����
public class OneMeleeTarget_Minion : Minion
{
    public override void Attack()
    {
        if (target == null) return;

        target.health -= damage;
    }
}
