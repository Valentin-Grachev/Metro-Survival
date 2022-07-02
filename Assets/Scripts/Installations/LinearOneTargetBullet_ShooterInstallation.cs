
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
        // ≈сли цель есть - стрел€ем в нее
        if (_target != null) instBullet.rb.velocity = (arrivalPoint - (Vector2)transform.position).normalized * _linearSpeed;
        // ≈сли ее нет - стрел€ем в последнее место где была цель
        else instBullet.rb.velocity = (_lastTargetPosition - (Vector2)transform.position).normalized * _linearSpeed;

        instBullet.team = _team;
        instBullet.damage = damage;


    }
}
