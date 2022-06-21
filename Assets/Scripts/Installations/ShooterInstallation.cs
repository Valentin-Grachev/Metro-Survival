using System.Collections;
using UnityEngine;

public abstract class ShooterInstallation : Installation
{
    [SerializeField] protected Team _team;
    [SerializeField] protected Transform _directionBone;
    [SerializeField] protected Transform _shotPoint;
    [SerializeField] protected GameObject _bullet;
    [SerializeField] protected float _detectionRadius;
    [SerializeField] protected LayerMask _enemyLayer;
    [SerializeField] protected float _attackSpeed;
    public float attackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
            _animator.SetFloat("AttackSpeed", value);
        }
    }

    protected Transform _target;

    protected const float scanInterval = 0.3f;
    


    public abstract void Shoot();


    protected override void Start()
    {
        base.Start();
        attackSpeed = attackSpeed;
        StartCoroutine(SearchTarget());
    }

    protected override void Update()
   {
        base.Update();
        if (_target != null)
        {
            Library.LookAtTarget2D(_directionBone, _target.position);
            _animator.SetBool("Attack", true);
        }
        else
        {
            _animator.SetBool("Attack", false);
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
