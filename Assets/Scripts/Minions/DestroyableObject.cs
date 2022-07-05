using UnityEngine;


public enum Team { Player, Enemy, Neutral }



public class DestroyableObject : MonoBehaviour
{
    //====== События ======

    public delegate void OnHealthChanged(int health, int maxHealth);
    public delegate void OnDeath();
    public event OnHealthChanged onHealthChanged;
    public OnDeath onDeath;



    //====== Настраиваемые характеристики ======
    [SerializeField] protected GameObject _localCanvas;
    [SerializeField] protected Collider2D _bulletCollider;
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
                if (!isDeath)
                {
                    isDeath = true;
                    onDeath?.Invoke();
                    //gameObject.layer = LayerMask.NameToLayer("Default");
                    animator.SetTrigger("Death");
                    if (_localCanvas != null) _localCanvas.SetActive(false);
                    if (_bulletCollider != null) _bulletCollider.enabled = false;
                }
            }
            else if (value > _maxHealth) _health = _maxHealth;
            else _health = value;
            onHealthChanged?.Invoke(_health, _maxHealth);
        }
    }


    //====== Свойства ======
    public bool isDeath { get; protected set; }
    public Animator animator { get; protected set; }



    private PoolObject _poolObject;


    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Возвращение из пула
    protected virtual void OnEnable()
    {
        isDeath = false;
        if (_localCanvas != null) _localCanvas.SetActive(true);
        if (_bulletCollider != null) _bulletCollider.enabled = true;

    }

    protected virtual void Start() 
    {
        
        health = maxHealth;
        isDeath = false;
        _poolObject = GetComponent<PoolObject>();
    }

    protected virtual void Update() { }

    protected virtual void OnDrawGizmosSelected() { }

}
