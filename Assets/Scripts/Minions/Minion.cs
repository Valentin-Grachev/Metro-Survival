using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;


public abstract class Minion : DestroyableObject
{
    //====== Изменяемые характеристики ======

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
    [SerializeField] protected bool _backDistract;
    [SerializeField] protected LayerMask _enemyLayer;

    


    //====== Свойства ======
    public Rigidbody2D rigidbody { get; protected set; }
    protected DestroyableObject _attackedTarget;
    public virtual DestroyableObject attackedTarget
    {
        get => _attackedTarget;
        protected set
        {
            _attackedTarget = value;
            print("Enter");
            // Если атакуемая цель существует - запускаем анимацию атаки
            if (_attackedTarget != null) spineAnimation.SetAnimation(AnimationType.Attack);
        }
    }

    public virtual Transform destination 
    { 
        get => _destination;
        set
        {
            _destination = value;

            // Если место назначения существует и некого атаковать - переходим в режим движения
            if (_destination != null && attackedTarget == null) spineAnimation.SetAnimation(AnimationType.Move);
        }
        
    }



    // ====== Другие параметры ======

    protected Transform _destination;
    protected List<DestroyableObject> _enemiesInsideAttackArea;





    // Активация из пула - инициализация заново
    protected override void FromPool()
    {
        base.FromPool();
        moveSpeed = moveSpeed;
        attackSpeed = attackSpeed;
        attackedTarget = null;
        if (team == Team.Enemy) destination = Trolley.instance.transform;
        else destination = null;
        _enemiesInsideAttackArea.Clear();
        
    }


    protected override void Start()
    {
        base.Start();
        _enemiesInsideAttackArea = new List<DestroyableObject>();
        rigidbody = GetComponent<Rigidbody2D>();

        // Стартовая обработка свойств
        moveSpeed = moveSpeed;
        attackSpeed = attackSpeed;
        if (team == Team.Enemy) destination = Trolley.instance.transform;
        else destination = null;
    }


    protected override void Run()
    {
        base.Run();

        // При смерти цели обнуляем ее
        if (attackedTarget != null && attackedTarget.isDeath) attackedTarget = null;

        // Если миньон не отвлекается на задние цели и при этом цель оказалась сзади - забываем ее
        if (!_backDistract && attackedTarget != null && transform.position.x < attackedTarget.transform.position.x)
            attackedTarget = null;




        // Если потеряли цель и в зоне атаки есть цель - выбираем новую
        if (attackedTarget == null && _enemiesInsideAttackArea.Count != 0)
        {
            SelectNewTarget();
            print("Search");
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Триггеры не учитываем
        if (collision.isTrigger) return;
        // Вражеский уничтожаемый объект вошел в зону - добавление в список
        if (team == Team.Enemy && collision.CompareTag("Allies") 
            || team == Team.Player && collision.CompareTag("Enemies"))
        {
            _enemiesInsideAttackArea.Add(collision.gameObject.GetComponent<DestroyableObject>());
        }
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Триггеры не учитываем
        if (collision.isTrigger) return;

        // Вражеский уничтожаемый объект вышел из зоны - извлечение из списка
        if (team == Team.Enemy && collision.CompareTag("Allies")
            || team == Team.Player && collision.CompareTag("Enemies"))
        {
            DestroyableObject destrObject = collision.gameObject.GetComponent<DestroyableObject>();
            // Если это был тот, кого атаковали - забываем его
            if (destrObject == _attackedTarget) attackedTarget = null;

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




    public abstract void Attack();


    // ====== Функции-состояния ======
    public void MoveLeft_UpdateState() => rigidbody.velocity = Vector2.left * moveSpeed;

    public void MoveLeft_ExitState() => rigidbody.velocity = Vector2.zero;

    public void AttackWithBackDistract_UpdateState()
    {
        if (attackedTarget == null) return;

        // Поворот миньона в сторону врага
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

        // При атаке стоит на месте
        rigidbody.velocity = Vector3.zero;
    }




}
