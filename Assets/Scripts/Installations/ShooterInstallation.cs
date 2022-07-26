using UnityEngine;

public abstract class ShooterInstallation : Installation
{
    public DirectionBone directionBone { get; protected set; }
    [SerializeField] protected Transform _shotPoint; public Transform shotPoint { get => _shotPoint;}
    protected Pool _bulletPool;
    public PoolObject bullet { get => _bulletPool.prefab; set => _bulletPool.prefab = value; }

    [SerializeField] protected float _attackSpeed;
    public float attackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
            spineAnimation.attackSpeed = value;
        }
    }
    [Min(0f)][SerializeField] protected float _shootingDeviation;

    [HideInInspector] public DestroyableObject target;
    protected bool targetIsAlive { get { return target != null && !target.isDeath; } } 
    protected Vector2 arrivalPoint
    { 
        get
        {
            Vector2 predictionPosition;

            // ≈сли цель жива - стрел€ем по ней с предсказанием
            if (targetIsAlive)
            {
                predictionPosition = target.pivot.position;
                //if (target is Minion) predictionPosition.x += ((Minion)target).velocityX * Constants.prediction;

            }

            // ≈сли цели нет, а выстрел произошел - стрел€ем по последней позиции цели
            else predictionPosition = _lastTargetPosition;
             
            return predictionPosition;
        }

    }
    protected Vector2 _lastTargetPosition;
    public int damage;


    public abstract void Shoot();


    protected override void Start()
    {
        base.Start();
        attackSpeed = attackSpeed;
        directionBone = GetComponent<DirectionBone>();
        _bulletPool = GetComponent<Pool>();
    }

    protected override void Run()
    {
        base.Run();

        // ≈сли цель жива - атакуем ее
        if (targetIsAlive)
        {
            directionBone.direction = ((Vector2)target.pivot.position - directionBone.bonePosition).normalized;
            if (spineAnimation.currentAnimation != AnimationType.Attack) spineAnimation.SetAnimation(AnimationType.Attack);
            _lastTargetPosition = target.pivot.position;
        }
        // Ќе жива, но есть в зоне атаки еще цели - выбираем новую
        else if (Library.TryFindNearestUntilLine(transform.position,
            BulletLimiter.instance.transform.position.x + BulletLimiter.instance.detectionLine, Team.Enemy, out DestroyableObject found))
        {
            target = found;
            directionBone.direction = ((Vector2)target.pivot.position - directionBone.bonePosition).normalized;
            if (spineAnimation.currentAnimation != AnimationType.Attack) spineAnimation.SetAnimation(AnimationType.Attack);
            _lastTargetPosition = target.pivot.position;
        }
        // Ќикого нет - прекращение атаки
        else
        {
            target = null;
            if (spineAnimation.currentAnimation != AnimationType.Idle) spineAnimation.SetAnimation(AnimationType.Idle);
        }


    }







    protected void OnDrawGizmosSelected()
    {
        // ќтрисовка отклонени€ пуль
        Gizmos.color = Color.magenta;
        Vector2 deviationUp = Quaternion.Euler(0f, 0f, _shootingDeviation * 0.5f) * Vector2.right;
        Vector2 deviationDown = Quaternion.Euler(0f, 0f, -_shootingDeviation * 0.5f) * Vector2.right;
        Gizmos.DrawRay(new Ray(transform.position, deviationUp*5f));
        Gizmos.DrawRay(new Ray(transform.position, deviationDown*5f));
    }



}
