using System.Collections;
using UnityEngine;

public abstract class ShooterInstallation : Installation
{
    [SerializeField] protected Team _team;
    public DirectionBone directionBone { get; protected set; }
    [SerializeField] protected Transform _shotPoint; public Transform shotPoint { get => _shotPoint;}
    public GameObject bullet;
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
    protected Vector2 arrivalPoint
    { 
        get
        {
            Rigidbody2D rb = _target.GetComponent<Rigidbody2D>();
            Vector2 predictionPosition = Library.GetTargetPositionWithPrediction(_target.position, rb.velocity, Vector2.Distance(transform.position, _target.position));
            predictionPosition.x += Random.Range(-_shootingDeviation, _shootingDeviation);
            predictionPosition.y += Random.Range(-_shootingDeviation, _shootingDeviation);
            return predictionPosition;
        }

    }
    public int damage;


    public abstract void Shoot();


    protected override void Start()
    {
        base.Start();
        attackSpeed = attackSpeed;
        StartCoroutine(SearchTarget());
        directionBone = GetComponent<DirectionBone>();
    }

    protected override void Update()
    {
        base.Update();
        if (_target != null)
        {
            directionBone.direction = ((Vector2)_target.position - directionBone.bonePosition).normalized;
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
        

    }




    protected IEnumerator SearchTarget()
    {
        while (true)
        {
            if (_target == null) _target = Library.SearchNearestCircle(transform.position, _detectionRadius, _enemyLayer);
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
