using Spine.Unity;
using System.Collections;
using UnityEngine;


public abstract class Minion : DestroyableObject
{
    //====== ���������� �������������� ======

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





    //====== �������� ======
    public Rigidbody2D rigidbody { get; protected set; }
    public DestroyableObject _attackedTarget { get; protected set; }
    public SkeletonMecanim mecanim { get; protected set; }




    //====== ��������� ======
    public const float moveAnimationCoeff = 0.5f; // �������� �������� ������ ��� ��������, ������ 1




    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        mecanim = GetComponent<SkeletonMecanim>();

        // ��������� ��������� �������
        moveSpeed = moveSpeed;
        attackSpeed = attackSpeed;

        StartCoroutine(ScanningForAttack());
    }

    protected override void Update()
    {
        base.Update();

        // TODO: ������� ����� �������

        if (_attackedTarget == null) animator.SetBool("Attacking", false);
        else animator.SetBool("Attacking", true);

        if (destination == null) animator.SetBool("Move", false);
        else animator.SetBool("Move", true);

    }


    IEnumerator ScanningForAttack()
    {
        while(true)
        {
            // ���� ���� ��� - ���� ��
            if (_attackedTarget == null)
            {
                Transform nearestEnemy = Library.SearchNearestBox(transform.position, _areaAttack, _enemyLayer);
                if (nearestEnemy != null && (_backDistract || (!_backDistract && (transform.position.x > nearestEnemy.position.x))))
                    _attackedTarget = nearestEnemy.GetComponent<DestroyableObject>();
                
            }
                
            
            // ���� ��� ���� - ���������, �� ����� �� ��� �� ������� ������������ �����
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
