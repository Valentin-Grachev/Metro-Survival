
using UnityEngine;

// ”становка-стрелок, стрел€юща€ пул€ми по пр€мой траектории
public class LinearOneTargetBullet_ShooterInstallation : ShooterInstallation
{
    [Header("Bullet Parameters:")]
    [SerializeField] protected float _linearSpeed;
    


    public override void Shoot()
    {

        OneTarget_Bullet instBullet = _bulletPool.GetElement
           (_shotPoint.position, Quaternion.identity).gameObject.GetComponent<OneTarget_Bullet>();

        // »нициализаци€ пули
        instBullet.rb = instBullet.gameObject.GetComponent<Rigidbody2D>();

        // «адаем направление пули с учетом возможных отклонений от пр€мой траектории
        Vector2 bulletDirection = (arrivalPoint - directionBone.bonePosition).normalized;
        bulletDirection = Quaternion.Euler(0f, 0f, Random.Range(-_shootingDeviation * 0.5f, _shootingDeviation * 0.5f)) * bulletDirection;
        instBullet.rb.velocity = bulletDirection * _linearSpeed;

        instBullet.team = _team;
        instBullet.damage = damage;


    }
}
