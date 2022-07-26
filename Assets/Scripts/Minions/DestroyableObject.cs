using NTC.Global.Cache;
using UnityEngine;


public enum Team { Ally, Enemy, Neutral }

[System.Serializable]
public struct BulletCollider
{
    public Vector2 size;
    public Vector2 offset;
}


public class DestroyableObject : PoolBehaviour
{
    //====== События ======

    public delegate void OnHealthChanged(int health, int maxHealth);
    public delegate void OnDeath();
    public event OnHealthChanged onHealthChanged;
    public OnDeath onDeath;

    [Header("Attachement Components:")]

    [Tooltip("В эту точку будут целиться все стрелки.")]
    [SerializeField] protected Transform _pivot;
    public Transform pivot { get => _pivot; }
    [SerializeField] protected GameObject[] _deathDeactiveObjects;

    [Header("Characteristics:")]
    [SerializeField] protected Team _team; public Team team { get => _team; }
    public Team enemyTeam
    {
        get
        {
            if (_team == Team.Ally) return Team.Enemy;
            else return Team.Ally;
        }
    } 
        
    [SerializeField] protected int _maxHealth; public int maxHealth { get => _maxHealth; set => _maxHealth = value; }
    protected int _health;
    public int health
    {
        get => _health;
        set
        {
            if (value <= 0) 
            { 
                // Смерть
                _health = 0;
                if (!isDeath) Death();
            }
            else if (value > _maxHealth) _health = _maxHealth;
            else _health = value;
            onHealthChanged?.Invoke(_health, _maxHealth);
        }
    }
    [SerializeField] protected BulletCollider _bulletCollider;
    public BulletCollider bulletCollider { get => _bulletCollider; protected set => _bulletCollider = value; }
    [SerializeField] protected AnimationType _startAnimation;

    //====== Свойства ======
    public bool isDeath { get; protected set; }
    public SpineAnimation spineAnimation { get; protected set; }



    


    protected virtual void Awake()
    {
        spineAnimation = GetComponent<SpineAnimation>();
    }


    protected override void FromPool()
    {
        base.FromPool();
        isDeath = false;
        health = maxHealth;
        for (int i = 0; i < _deathDeactiveObjects.Length; i++) _deathDeactiveObjects[i].SetActive(true);
        spineAnimation?.SetAnimation(_startAnimation);

        if (team == Team.Enemy) AllMinions.instance.enemies.Add(this);
        else if (team == Team.Ally) AllMinions.instance.allies.Add(this);
    }


    protected virtual void Death()
    {
        isDeath = true;
        onDeath?.Invoke();
        spineAnimation.SetAnimation(AnimationType.Death);
        if (team == Team.Enemy) AllMinions.instance.enemies.Remove(this);
        else if (team == Team.Ally) AllMinions.instance.allies.Remove(this);
        for (int i = 0; i < _deathDeactiveObjects.Length; i++) _deathDeactiveObjects[i].SetActive(false);
    }

    protected virtual void OnDestroy()
    {
        if (team == Team.Enemy) AllMinions.instance.enemies.Remove(this);
        else if (team == Team.Ally) AllMinions.instance.allies.Remove(this);
    }


    public void Destroy() => Destroy(gameObject);


    protected virtual void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + _bulletCollider.offset, _bulletCollider.size);
    }




}
