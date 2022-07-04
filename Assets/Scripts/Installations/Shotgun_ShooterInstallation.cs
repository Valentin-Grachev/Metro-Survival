using UnityEngine;

public class Shotgun_ShooterInstallation : ShooterInstallation
{
    [Header("Bullet Parameters:")]
    [SerializeField] protected float _linearSpeed;
    [SerializeField] protected int _bulletQuantity;


    public override void Shoot()
    {
        // —оздание нескольких пуль
        for (int i = 0; i < _bulletQuantity; i++)
        {
            OneTarget_Bullet instBullet = _bulletPool.GetElement
            (_shotPoint.position, Quaternion.identity).gameObject.GetComponent<OneTarget_Bullet>();

            // »нициализаци€ пули
            instBullet.rb = instBullet.gameObject.GetComponent<Rigidbody2D>();

            // «адаем направление пули с учетом возможных отклонение от пр€мой траектории
            Vector2 bulletDirection = (arrivalPoint - (Vector2)transform.position).normalized;
            bulletDirection = Quaternion.Euler(0f, 0f, Random.Range(-_shootingDeviation * 0.5f, _shootingDeviation * 0.5f)) * bulletDirection;
            instBullet.rb.velocity = bulletDirection * _linearSpeed;

            instBullet.team = _team;
            instBullet.damage = damage;
        }
        
    }
}
