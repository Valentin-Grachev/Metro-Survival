
using UnityEngine;

// ”становка-стрелок, стрел€юща€ пул€ми по пр€мой траектории
public class LinearOneTargetBullet_ShooterInstallation : ShooterInstallation
{
    [Header("Bullet Parameters:")]
    [SerializeField] protected float _linearSpeed;
    [SerializeField] protected int _damage;

    public override void Shoot()
    {
        if (_target == null) return;
        LinearOneTargetBullet bullet = Instantiate(_bullet, _shotPoint.position, Quaternion.identity).GetComponent<LinearOneTargetBullet>();

        Library.LookAtTarget2D(bullet.transform, _target.position);
        bullet.rb = bullet.gameObject.GetComponent<Rigidbody2D>();
        bullet.rb.velocity = (_target.position - transform.position).normalized * _linearSpeed;
        bullet.team = _team;
        bullet.damage = _damage;


    }
}
