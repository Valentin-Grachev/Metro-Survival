using NTC.Global.Cache;
using UnityEngine;


public enum Team { Player, Enemy, Neutral }



public class DestroyableObject : PoolBehaviour
{
    //====== События ======

    public delegate void OnHealthChanged(int health, int maxHealth);
    public delegate void OnDeath();
    public event OnHealthChanged onHealthChanged;
    public OnDeath onDeath;



    //====== Настраиваемые характеристики ======
    [Header("Attachement Components:")]
    [SerializeField] protected GameObject _localCanvas;
    [SerializeField] protected Collider2D _bulletCollider;

    [Header("Characteristics:")]
    [SerializeField] protected Team _team; public Team team { get => _team; }
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
    [SerializeField] protected AnimationType _startAnimation;

    //====== Свойства ======
    public bool isDeath { get; protected set; }
    public SpineAnimation spineAnimation { get; protected set; }


    // ====== Стартовые значения для реинициализации ======
    protected int _startLayer;





    protected virtual void Awake()
    {
        spineAnimation = GetComponent<SpineAnimation>();
    }


    protected override void FromPool()
    {
        base.FromPool();
        isDeath = false;
        if (_startLayer != LayerMask.NameToLayer("Default")) gameObject.layer = _startLayer;
        if (_localCanvas != null) _localCanvas.SetActive(true);
        if (_bulletCollider != null) _bulletCollider.enabled = true;
        spineAnimation.SetAnimation(_startAnimation);
    }

    protected override void Run()
    {
        base.Run();
    }

    protected virtual void Start() 
    {
        health = maxHealth;
        isDeath = false;
        _startLayer = gameObject.layer;
        spineAnimation.SetAnimation(_startAnimation);

    }

    protected virtual void Death()
    {
        isDeath = true;
        onDeath?.Invoke();
        gameObject.layer = LayerMask.NameToLayer("Default");
        spineAnimation.SetAnimation(AnimationType.Death);
        if (_localCanvas != null) _localCanvas.SetActive(false);
        if (_bulletCollider != null) _bulletCollider.enabled = false;
    }



    protected virtual void OnDrawGizmosSelected() { }

}
