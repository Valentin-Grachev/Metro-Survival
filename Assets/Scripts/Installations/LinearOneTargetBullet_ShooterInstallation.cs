
using UnityEngine;

// ���������-�������, ���������� ������ �� ������ ����������
public class LinearOneTargetBullet_ShooterInstallation : ShooterInstallation
{
    [Header("Bullet Parameters:")]
    [SerializeField] protected float _linearSpeed;
    


    public override void Shoot()
    {
        
        OneTarget_Bullet instBullet = _bulletPool.GetElement
            (_shotPoint.position, Quaternion.identity).gameObject.GetComponent<OneTarget_Bullet>();

        // ������������� ����
        instBullet.rb = instBullet.gameObject.GetComponent<Rigidbody2D>();
        // ���� ���� ���� - �������� � ���
        if (_target != null) instBullet.rb.velocity = (arrivalPoint - (Vector2)transform.position).normalized * _linearSpeed;
        // ���� �� ��� - �������� � ��������� ����� ��� ���� ����
        else instBullet.rb.velocity = (_lastTargetPosition - (Vector2)transform.position).normalized * _linearSpeed;

        instBullet.team = _team;
        instBullet.damage = damage;


    }
}
