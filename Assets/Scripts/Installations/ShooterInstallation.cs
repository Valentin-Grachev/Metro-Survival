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

    protected Transform _target;
    protected Transform target 
    { 
        get => _target;
        set
        { 
            // Если удаляется ранее известная цель - запоминаем ее последнюю позицию
            if (value == null && _target != null) _lastTargetPosition = _target.position;
            _target = value;

        } 
    }
    protected Vector2 arrivalPoint
    { 
        get
        {
            Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
            Vector2 predictionPosition = Library.GetTargetPositionWithPrediction(target.position, rb.velocity, Vector2.Distance(transform.position, target.position));
            predictionPosition.x += Random.Range(-_shootingDeviation, _shootingDeviation);
            predictionPosition.y += Random.Range(-_shootingDeviation, _shootingDeviation);
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
        StartCoroutine(SearchTarget());
        directionBone = GetComponent<DirectionBone>();
        _bulletPool = GetComponent<Pool>();
    }

    protected override void Update()
    {
        base.Update();
        if (target != null && target.gameObject.activeInHierarchy)
        {
            directionBone.direction = ((Vector2)target.position - directionBone.bonePosition).normalized;
            animator.SetBool("Attack", true);
        }
        else
        {
            target = null;
            animator.SetBool("Attack", false);
        }
        

    }




    protected IEnumerator SearchTarget()
    {
        while (true)
        {
            target = Library.SearchNearestCircle(transform.position, _detectionRadius, _enemyLayer);
            yield return new WaitForSeconds(Constants.scanInterval);
        }
    }





    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + new Vector3(3f, 1f, 0f),  new Vector3(2*_shootingDeviation, 2*_shootingDeviation, 0f));
    }



}
