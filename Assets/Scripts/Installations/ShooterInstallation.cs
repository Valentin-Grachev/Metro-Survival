using System.Collections;
using UnityEngine;

public abstract class ShooterInstallation : Installation
{
    [SerializeField] protected Team _team;
    public DirectionBone directionBone { get; protected set; }
    [SerializeField] protected Transform _shotPoint; public Transform shotPoint { get => _shotPoint;}
    [SerializeField] protected GameObject _bullet;
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

    protected Transform _target;


    protected const float scanInterval = 0.3f;
    protected const float turningSpeed = 10f;


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
            if (_target == null) _target = Library.SearchNearest(transform.position, _detectionRadius, _enemyLayer);
            yield return new WaitForSeconds(scanInterval);
        }
    }





    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }



}
