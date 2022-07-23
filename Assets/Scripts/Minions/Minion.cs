using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;


public abstract class Minion : DestroyableObject
{
    #region Fields and Properties 

    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _normalizeMoveAnimationSpeed;
    public virtual float moveSpeed
    {
        get => _moveSpeed;
        set
        {
            _moveSpeed = value;
            spineAnimation.moveSpeed = value * _normalizeMoveAnimationSpeed;
        }
    }
    [SerializeField] protected float _attackSpeed;
    public float attackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
            spineAnimation.attackSpeed = value;
        }
    }

    public int damage;
    [SerializeField] protected Vector2 _attackArea;


    protected DestroyableObject _attackedTarget;
    public bool attackedTargetIsAlive { get => _attackedTarget != null && !_attackedTarget.isDeath; }
    public virtual DestroyableObject attackedTarget
    {
        get => _attackedTarget;
        protected set
        {
            _attackedTarget = value;

            // Если цель существует - устанавливаем анимацию атаки
            if (_attackedTarget != null) spineAnimation.SetAnimation(AnimationType.Attack);
        }
    }

    protected Transform _destination;
    public virtual Transform destination 
    { 
        get => _destination;
        set
        {
            _destination = value;
            // Если существует точка назначения и цели для атаки нет, то устанавливаем анимацию движения
            if (_destination != null && !attackedTargetIsAlive) spineAnimation.SetAnimation(AnimationType.Move);
        }
        
    }

    private float _oldPositionX = 0f;
    public float velocityX { get; private set; }

    #endregion


    private float CalculateVelocityX(float oldPositionX, float newPositionX, float time)
    {
        if (oldPositionX == 0f) return 0f;
        else return (newPositionX - oldPositionX) / time;

    }
    




    // В методе Start() отработает FromPool() 
    protected override void FromPool()
    {
        base.FromPool();
        moveSpeed = moveSpeed;
        attackSpeed = attackSpeed;
        attackedTarget = null;
        if (team == Team.Enemy) destination = Trolley.instance.transform;
        else destination = null;
        
    }



    protected override void Run()
    {
        base.Run();

        // Обновление скорости
        CalculateVelocityX(_oldPositionX, transform.position.x, Time.deltaTime);
        _oldPositionX = transform.position.x;


        // Если нет цели для атаки - обнуляем ее и ищем новую
        if (!attackedTargetIsAlive)
        {
            attackedTarget = null;  // Необходимо для запуска анимации

            if (Library.TryFindNearestInsideArea(transform.position, _attackArea, enemyTeam, out DestroyableObject found))
                attackedTarget = found;

        }

        // Если атакуемый объект - цель, на которую не нужно отвлекаться, если она находится сзади - обнуляем цель
        else if (attackedTarget.CompareTag("NoBackDistract") && transform.position.x < attackedTarget.transform.position.x)
            attackedTarget = null;

    }




    public abstract void Attack();


    // ====== Функции-сocтояния ======
    public void MoveLeft_UpdateState() => transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

    public void LookAtAttackedTarget_UpdateState()
    {
        if (!attackedTargetIsAlive) return;

        Vector2 moveDirection = (attackedTarget.transform.position - transform.position).normalized;
        if (moveDirection.x < 0 && !spineAnimation.skeletonAnimation.initialFlipX)
        {
            spineAnimation.skeletonAnimation.initialFlipX = true;
            spineAnimation.skeletonAnimation.Initialize(true);
        }

        else if (moveDirection.x >= 0 && spineAnimation.skeletonAnimation.initialFlipX)
        {
            spineAnimation.skeletonAnimation.initialFlipX = false;
            spineAnimation.skeletonAnimation.Initialize(true);
        }

    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, _attackArea);

    }


}
