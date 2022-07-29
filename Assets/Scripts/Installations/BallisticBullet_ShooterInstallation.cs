using UnityEngine;

public class BallisticBullet_ShooterInstallation : ShooterInstallation
{ 
    [Tooltip("На сколько градусов поднять угол выстрела относительно прямой, соединяющей стрелка и цель.")]
    [SerializeField] protected float _elevationAngle;

    [Header("Bullet Parameters:")]
    [SerializeField] protected Vector2 _damageArea;

    protected float _launchAngleInDegrees;


    public override void Shoot()
    {
        print("shoot");
        Explosive_PhysicalBullet bullet = bulletPool.GetElement(_shotPoint.position, Quaternion.identity).GetComponent<Explosive_PhysicalBullet>();
        bullet.rigidbody = bullet.gameObject.GetComponent<Rigidbody2D>();
        bullet.damage = damage;
        bullet.damageArea = _damageArea;
        bullet.teamForCollide = Team.Enemy;


        Vector2 destination;

        if (targetIsAlive)
            destination = (Vector2)target.pivot.position + new Vector2(((Minion)target).velocityX * Constants.prediction, 0f)
                + new Vector2(_damageArea.x * Constants.backOffset, 0f);

        else destination = _lastTargetPosition + new Vector2(((Minion)target).velocityX * Constants.prediction, 0f)
                + new Vector2(_damageArea.x * Constants.backOffset, 0f);

        float startSpeed = Library.GetStartSpeedForBallisticBullet(_shotPoint.position, destination, _launchAngleInDegrees, bullet.rigidbody.gravityScale);
        bullet.rigidbody.velocity = directionBone.direction * startSpeed;

    }


    protected override void Run()
    {
        base.Run();
        
        if (targetIsAlive)
        {
            Vector2 direction = ((Vector2)target.pivot.position - directionBone.bonePosition).normalized;
            direction = Quaternion.Euler(0f, 0f, _elevationAngle) * direction;
            if (direction.y > 0f) _launchAngleInDegrees = Vector2.Angle(direction, Vector2.right);
            else _launchAngleInDegrees = -Vector2.Angle(direction, Vector2.right);
            directionBone.direction = direction;
        }
            
    }



    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, _damageArea);
    }



}
