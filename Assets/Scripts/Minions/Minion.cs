using Spine.Unity;
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

    [SerializeField] protected Transform _destination;
    public virtual Transform destination { get => _destination; set => _destination = value; }





    //====== Свойства ======
    public Rigidbody2D rigidbody { get; protected set; }
    public DestroyableObject target { get; protected set; }
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
    }

    protected override void Update()
    {
        base.Update();

        // TODO: Сделать через события

        if (target == null) animator.SetBool("Attacking", false);
        else animator.SetBool("Attacking", true);

        if (destination == null) animator.SetBool("Move", false);
        else animator.SetBool("Move", true);

    }



    // Враг вошел в зону ближнего боя, при этом миньон до этого не бил по другому врагу
    // TODO: переделать на OverlapBox
    protected virtual void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.transform.TryGetComponent(out DestroyableObject enemyObject) && enemyObject.team != team && target == null)
        {
            if (!_backDistract && transform.position.x < collider.transform.position.x) return;
            target = enemyObject;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.TryGetComponent(out Minion minion) && minion == target)
            target = null;
    }

    public abstract void Attack();





}
