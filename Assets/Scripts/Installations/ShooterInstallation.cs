using System.Collections;
using UnityEngine;

public abstract class ShooterInstallation : Installation
{
    [SerializeField] protected Team _team;
    public DirectionBone directionBone { get; protected set; }
    [SerializeField] protected Transform _shotPoint; public Transform shotPoint { get => _shotPoint;}
    protected Pool _bulletPool;
    public PoolObject bullet { get => _bulletPool.prefab; set => _bulletPool.prefab = value; }
    [SerializeField] protected float _detectionRadius; public float detectionRadius { get => _detectionRadius; }
    [SerializeField] protected LayerMask _enemyLayer; public LayerMask enemyLayer { get => _enemyLayer; }
    [SerializeField] protected float _attackSpeed;
    public float attackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
            animator.SetFloat("AttackSpeed", value);
        }
    }
    [Min(0f)][SerializeField] protected float _shootingDeviation;

    protected DestroyableObject _target;
    protected DestroyableObject target 
    { 
        get => _target;
        set
        { 
            // ≈сли удал€етс€ ранее известна€ цель - запоминаем ее последнюю позицию
            if (value == null && _target != null) _lastTargetPosition = _target.transform.position;
            _target = value;

        } 
    }
    protected Vector2 arrivalPoint
    { 
        get
        {
            Vector2 predictionPosition;

            // ≈сли цель известна - стрел€ем по ней с предсказанием
            if (_target != null)
            {
                Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
                predictionPosition = Library.GetTargetPositionWithPrediction(target.transform.position, rb.velocity, Vector2.Distance(transform.position, target.transform.position));
            }

            // ≈сли цели нет, а выстрел произошел - стрел€ем по последней позиции цели
            else predictionPosition = _lastTargetPosition;
             
            predictionPosition.y += Constants.pivotUpForAiming;
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

        // ≈сли цель жива и находитс€ на достаточном дл€ стрельбы рассто€нии - атакуем ее
        if (target != null && !target.isDeath && Vector2.Distance(transform.position, target.transform.position) < detectionRadius)
        {
            directionBone.direction = ((Vector2)target.transform.position + new Vector2(0f,Constants.pivotUpForAiming) - directionBone.bonePosition).normalized;
            animator.SetBool("Attack", true);
        }
        else
        {
            target = null;
            animator.SetBool("Attack", false);
        }
        

        if (target == null) 
            target = Library.SearchNearestCircle(transform.position, _detectionRadius, _enemyLayer)?.GetComponent<DestroyableObject>();



    }







    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);


        // ќтрисовка отклонени€ пуль
        Gizmos.color = Color.magenta;
        Vector2 deviationUp = Quaternion.Euler(0f, 0f, _shootingDeviation * 0.5f) * Vector2.right;
        Vector2 deviationDown = Quaternion.Euler(0f, 0f, -_shootingDeviation * 0.5f) * Vector2.right;
        Gizmos.DrawRay(new Ray(transform.position, deviationUp*5f));
        Gizmos.DrawRay(new Ray(transform.position, deviationDown*5f));
    }



}
