
using UnityEngine;

// ”становка-стрелок, стрел€юща€ пул€ми по пр€мой траектории
public class LinearOneTargetBullet_ShooterInstallation : ShooterInstallation
{
    [Header("Bullet Parameters:")]
    [SerializeField] protected float _linearSpeed;
    


    public override void Shoot()
    {
        if (_target == null) return;
        OneTarget_Bullet instBullet = Instantiate(bullet, _shotPoint.position, Quaternion.identity).GetComponent<OneTarget_Bullet>();


        instBullet.rb = instBullet.gameObject.GetComponent<Rigidbody2D>();

        instBullet.rb.velocity = (arrivalPoint - (Vector2)transform.position).normalized * _linearSpeed;
        instBullet.team = _team;
        instBullet.damage = damage;


    }
}
