using UnityEngine;


public abstract class Minion : DestroyableObject
{

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
        protected set => _attackedTarget = value;
    }

    protected DestroyableObject _destination;
    protected bool destinationIsAlive { get => destination != null && destination.isDeath == false; }
    public virtual DestroyableObject destination 
    { 
        get => _destination;
        set => _destination = value;
    }

    private float _oldPositionX = 0f;
    public float velocityX { get; private set; }



    private float CalculateVelocityX(float oldPositionX, float newPositionX, float time)
    {
        if (oldPositionX == 0f) return 0f;
        else return (newPositionX - oldPositionX) / time;

    }
    




    // В методе Start() отработает FromPool() 
    protected override void FromPool()
    {
        base.FromPool();
        moveSpeed = Random.Range(moveSpeed - moveSpeed*Constants.individualization, moveSpeed + moveSpeed * Constants.individualization);
        attackSpeed = Random.Range(attackSpeed - attackSpeed * Constants.individualization, attackSpeed + attackSpeed * Constants.individualization);
        attackedTarget = null;
        if (team == Team.Enemy) destination = Trolley.instance;
        else destination = null;
        
    }



    protected override void Run()
    {
        base.Run();

        // Обновление скорости
        velocityX = CalculateVelocityX(_oldPositionX, transform.position.x, Time.deltaTime);
        _oldPositionX = transform.position.x;

        if (isDeath) return;

        // Запуск анимаций
        if (attackedTargetIsAlive) spineAnimation.SetAnimation(AnimationType.Attack);
        else if (destinationIsAlive) spineAnimation.SetAnimation(AnimationType.Move); 
        else spineAnimation.SetAnimation(AnimationType.Idle);


        // Если атакуемая цель вышла из зоны атаки - обнуляем ее
        if (attackedTargetIsAlive && !Library.ObjectIsInsideArea(attackedTarget.transform.position, transform.position, _attackArea)) 
            attackedTarget = null;

        // Если нет цели для атаки - обнуляем ее и ищем новую
        if (!attackedTargetIsAlive)
        {
            if (Library.TryFindNearestInsideArea(transform.position, _attackArea, enemyTeam, out DestroyableObject found))
                attackedTarget = found;
        }

        // Если атакуемый объект - цель, на которую не нужно отвлекаться и если она находится сзади - обнуляем цель
        if (attackedTargetIsAlive && attackedTarget.CompareTag("NoBackDistract") && transform.position.x < attackedTarget.transform.position.x)
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
