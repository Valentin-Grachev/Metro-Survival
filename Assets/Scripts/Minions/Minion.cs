using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;


public abstract class Minion : DestroyableObject
{
    //====== ���������� �������������� ======

    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _normalizeMoveAnimationSpeed;
    public virtual float moveSpeed 
    {
        get => _moveSpeed;
        set
        {
            _moveSpeed = value;
            animator.SetFloat("MoveSpeed", value * _normalizeMoveAnimationSpeed);
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
    [SerializeField] protected LayerMask _enemyLayer;

    


    //====== �������� ======
    public Rigidbody2D rigidbody { get; protected set; }
    protected DestroyableObject _attackedTarget;
    public virtual DestroyableObject attackedTarget
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



    // ====== ������ ��������� ======

    [SerializeField] protected Transform _destination;
    protected List<DestroyableObject> _enemiesInsideAttackArea;





    // ��������� �� ���� - ������������� ������
    protected override void OnEnabled()
    {
        base.OnEnabled();
        moveSpeed = moveSpeed;
        attackSpeed = attackSpeed;
        attackedTarget = null;
        if (team == Team.Enemy)
        {
            destination = Trolley.instance.transform;
            
        }
        else destination = null;
        _enemiesInsideAttackArea?.Clear();
        print(destination);
        
    }


    protected override void Start()
    {
        base.Start();
        _enemiesInsideAttackArea = new List<DestroyableObject>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        mecanim = GetComponent<SkeletonMecanim>();


        //if (team == Team.Enemy) destination = Trolley.instance.transform;
        //else destination = null;

        // ��������� ��������� �������
        moveSpeed = moveSpeed;
        attackSpeed = attackSpeed;
    }


    protected override void Run()
    {
        base.Run();

        // ��� ������ ���� �������� ��
        if (attackedTarget != null && attackedTarget.isDeath)
        {
            attackedTarget = null;
        }

        // ���� ������ �� ����������� �� ������ ���� � ��� ���� ���� ��������� ����� - �������� ��
        if (!_backDistract && attackedTarget != null && transform.position.x < attackedTarget.transform.position.x)
            attackedTarget = null;




        // ���� �������� ����
        if (attackedTarget == null || !attackedTarget.gameObject.activeInHierarchy)
        {
            // � ���� ����� ���� ���� - �������� �����
            if (_enemiesInsideAttackArea.Count != 0) SelectNewTarget();

            // ������ ��� - ���������� ���������
            else animator.SetBool("Attacking", false);

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��������� ������������ ������ ����� � ���� - ���������� � ������
        if (collision.gameObject.TryGetComponent(out DestroyableObject destrObject))
        {
            if (destrObject.team != team) _enemiesInsideAttackArea.Add(destrObject);
            
        }
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ��������� ������������ ������ ����� �� ���� - ���������� �� ������
        if (collision.gameObject.TryGetComponent(out DestroyableObject destrObject))
        {
            // ���� ��� ��� ���, ���� ��������� - �������� ���
            if (destrObject == _attackedTarget) _attackedTarget = null;

            if (destrObject.team != team) _enemiesInsideAttackArea.Remove(destrObject);
        }
    }


    private void SelectNewTarget()
    {
        foreach (var item in _enemiesInsideAttackArea)
        {
            if (!_backDistract)
            {
                if (transform.position.x > item.transform.position.x && !item.isDeath)
                {
                    attackedTarget = item;
                    return;
                }

            }
            else if (!item.isDeath)
            {
                attackedTarget = item;
                return;
            }
                
        }
    }


    private void OnDeath_AttackedMinion() => attackedTarget = null;


    public abstract void Attack();




}
