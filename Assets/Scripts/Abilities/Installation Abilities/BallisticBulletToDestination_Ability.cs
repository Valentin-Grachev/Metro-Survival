using UnityEngine;

public class BallisticBulletToDestination_Ability : ControlledAbility
{
    [Header("Common Parameters:")]
    [SerializeField] protected GameObject _bullet;
    [SerializeField] protected float _elevationAngle;
    [SerializeField] protected Transform _shotPoint;

    [Tooltip("Ѕудет ли измен€тьс€ направление оружи€ во врем€ работы анимации способности")]
    [SerializeField] protected bool _rotateDirection;
    

    [Header("Bullet Parameters:")]
    [SerializeField] protected int _damage;
    [SerializeField] protected Vector2 _damageArea;

    protected float _launchAngleInDegrees;
    protected Vector2 _launchDirection;



    public override void Active()
    {
        Explosive_PhysicalBullet bullet = Instantiate(_bullet, _shotPoint.position, Quaternion.identity).GetComponent<Explosive_PhysicalBullet>();
        bullet.rigidbody = bullet.gameObject.GetComponent<Rigidbody2D>();
        bullet.damage = _damage;
        bullet.damageArea = _damageArea;
        bullet.teamForCollide = Team.Enemy;

        


        float startSpeed = Library.GetStartSpeedForBallisticBullet(_shotPoint.position, destination, _launchAngleInDegrees, bullet.rigidbody.gravityScale);
        
        bullet.rigidbody.velocity = _launchDirection * startSpeed;
        
    }


    public override void Enable()
    {
        base.Enable();
        timeUntilRecharge = _rechargeTime;

        // –асчет угла запуска снар€да
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        direction = Quaternion.Euler(0f, 0f, _elevationAngle) * direction;
        if (direction.y > 0f) _launchAngleInDegrees = Vector2.Angle(direction, Vector2.right);
        else _launchAngleInDegrees = -Vector2.Angle(direction, Vector2.right);


        _launchDirection = Quaternion.Euler(0f, 0f, _launchAngleInDegrees) * Vector2.right;
        _launchDirection.Normalize();
        if (_rotateDirection) _installation.directionBone.direction = _launchDirection;
    }

    

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, _damageArea);
    }



}
