using UnityEngine;

public class BallisticBulletToDestination_Ability : ControlledAbility
{
    [Header("Common Parameters:")]
    [SerializeField] protected GameObject _bullet;
    [SerializeField] protected float _elevationAngle;
    [SerializeField] protected Transform _shotPoint;
    

    [Header("Bullet Parameters:")]
    [SerializeField] protected int _damage;
    [SerializeField] protected int _damageRadius;

    protected float _launchAngleInDegrees;



    public override void Active()
    {
        Explosive_Bullet bullet = Instantiate(_bullet, _shotPoint.position, Quaternion.identity).GetComponent<Explosive_Bullet>();
        bullet.rb = bullet.gameObject.GetComponent<Rigidbody2D>();
        bullet.damage = _damage;
        bullet.damageRadius = _damageRadius;

        


        float startSpeed = Library.GetStartSpeedForBallisticBullet(_shotPoint.position, destination, _launchAngleInDegrees, bullet.rb.gravityScale);
        
        bullet.rb.velocity = _installation.directionBone.direction * startSpeed;
        
    }


    public override void Enable()
    {
        base.Enable();
        timeUntilRecharge = _rechargeTime;

        // Расчет угла запуска снаряда
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        direction = Quaternion.Euler(0f, 0f, _elevationAngle) * direction;
        if (direction.y > 0f) _launchAngleInDegrees = Vector2.Angle(direction, Vector2.right);
        else _launchAngleInDegrees = -Vector2.Angle(direction, Vector2.right);


        Vector2 newDirection = Quaternion.Euler(0f, 0f, _launchAngleInDegrees) * Vector2.right;
        _installation.directionBone.direction = newDirection;
    }

    

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _damageRadius);
    }



}
