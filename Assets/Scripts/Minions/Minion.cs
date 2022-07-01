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
            animator.SetFloat("MoveSpeed", value* moveAnimationCoeff);
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

    [SerializeField] protected Transform _destination;
    public virtual Transform destination { get => _destination; set => _destination = value; }





    //====== Свойства ======
    public Rigidbody2D rigidbody { get; protected set; }
    public DestroyableObject _attackedTarget { get; protected set; }
    public SkeletonMecanim mecanim { get; protected set; }




    //====== Константы ======
    public const float moveAnimationCoeff = 0.5f; // Скорость анимации ходьбы при скорости, равной 1




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

    protected override void Update()
    {
        base.Update();

        // TODO: Сделать через события

        if (_attackedTarget == null) animator.SetBool("Attacking", false);
        else animator.SetBool("Attacking", true);

        if (destination == null) animator.SetBool("Move", false);
        else animator.SetBool("Move", true);

    }


    IEnumerator ScanningForAttack()
    {
        while(true)
        {
            // Если цели нет - ищем ее
            if (_attackedTarget == null)
            {
                Transform nearestEnemy = Library.SearchNearestBox(transform.position, _areaAttack, _enemyLayer);
                if (nearestEnemy != null && (_backDistract || (!_backDistract && (transform.position.x > nearestEnemy.position.x))))
                    _attackedTarget = nearestEnemy.GetComponent<DestroyableObject>();
                
            }
                
            
            // Если она есть - проверяем, не вышла ли она за область досягаемости атаки
            else if (!Library.SearchTransformInScanningBox(_attackedTarget.transform, transform.position, _areaAttack, _enemyLayer))
                _attackedTarget = null;

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
