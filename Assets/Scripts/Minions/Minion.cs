using Spine.Unity;
using System.Collections;
using UnityEngine;


public abstract class Minion : DestroyableObject
{
    //====== Изменяемые характеристики ======

    [SerializeField] protected float _moveSpeed;
    public virtual float moveSpeed 
    {
        get => _moveSpeed;
        set
        {
            _moveSpeed = value;
            animator.SetFloat("MoveSpeed", value * moveAnimationCoeff);
        }
    }
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

    public int damage;
    [SerializeField] protected bool _backDistract;
    [SerializeField] protected Vector2 _areaAttack;
    [SerializeField] protected LayerMask _enemyLayer;

    protected Transform _destination;
    


    //====== Свойства ======
    public Rigidbody2D rigidbody { get; protected set; }
    protected DestroyableObject _attackedTarget;
    public DestroyableObject attackedTarget
    {
        get => _attackedTarget;
        protected set
        {
            _attackedTarget = value;
            if (_attackedTarget == null) animator.SetBool("Attacking", false);
            else animator.SetBool("Attacking", true);
        }
    }

    public virtual Transform destination 
    { 
        get => _destination;
        set
        {
            _destination = value;
            if (_destination == null) animator.SetBool("Move", false);
            else animator.SetBool("Move", true);
        }
        
    }
    public SkeletonMecanim mecanim { get; protected set; }




    //====== Константы ======
    public const float moveAnimationCoeff = 0.5f; // Скорость анимации ходьбы при скорости, равной 1


    // Активация из пула - восстановление исходных данных
    
    private void OnEnable()
    {
        // Стартовая обработка свойств
        moveSpeed = moveSpeed;
        attackSpeed = attackSpeed;
        attackedTarget = null;
        _destination = null;
    }


    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        mecanim = GetComponent<SkeletonMecanim>();

        // Стартовая обработка свойств
        moveSpeed = moveSpeed;
        attackSpeed = attackSpeed;

        StartCoroutine(ScanningForAttack());
    }




    IEnumerator ScanningForAttack()
    {
        while(true)
        {
            // Если цели нет - ищем ее
            if (attackedTarget == null)
            {
                Transform nearestEnemy = Library.SearchNearestBox(transform.position, _areaAttack, _enemyLayer);
                if (nearestEnemy != null && (_backDistract || (!_backDistract && (transform.position.x > nearestEnemy.position.x))))
                    attackedTarget = nearestEnemy.GetComponent<DestroyableObject>();
                
            }
                
            // Если она есть - проверяем, не вышла ли она за область досягаемости атаки
            else if (!Library.SearchTransformInScanningBox(attackedTarget.transform, transform.position, _areaAttack, _enemyLayer))
                attackedTarget = null;

            yield return new WaitForSeconds(Constants.scanInterval);
        }
    }



    public abstract void Attack();


    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(_areaAttack.x, _areaAttack.y, 0f));
    }


}
