using UnityEngine;

public class DoubleShotPoint_ShooterInstallation : LinearOneTargetBullet_ShooterInstallation
{
    [SerializeField] protected Transform _secondShotPoint;
    protected bool leftShot;

    public override void Shoot()
    {
        if (leftShot) base.Shoot();
        else
        {
            OneTarget_Bullet instBullet2 = bulletPool.GetElement
           (_secondShotPoint.position, Quaternion.identity).gameObject.GetComponent<OneTarget_Bullet>();

            // Задаем направление пули с учетом возможных отклонений от прямой траектории
            Vector2 bulletDirection = directionBone.currentDirection.normalized;
            bulletDirection = Quaternion.Euler(0f, 0f, Random.Range(-_shootingDeviation * 0.5f, _shootingDeviation * 0.5f)) * bulletDirection;
            instBullet2.velocity = bulletDirection * _linearSpeed;

            instBullet2.teamForCollide = Team.Enemy;
            instBullet2.damage = damage;
        }

        leftShot = !leftShot;
    }


}
