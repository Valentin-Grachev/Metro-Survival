using UnityEngine;

public class LinearOneTargetBullet : Bullet
{
    [HideInInspector] public int damage;

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        // ������ �� ���������� �������
        if (collider.transform.parent.TryGetComponent(out Minion minion))
        {
            if (minion.team != team) minion.health -= damage;
        }
        // ����� ��� ����� ��������� � ��������� ��� ����
        _animator.SetTrigger("Collision");
        rb.velocity = Vector3.zero;
    }
}
