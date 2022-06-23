using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Team { Player, Enemy }



public class DestroyableObject : MonoBehaviour
{
    //====== События ======

    public delegate void OnHealthChanged(int health, int maxHealth);
    public delegate void OnDeath();
    public event OnHealthChanged onHealthChanged;
    public OnDeath onDeath;



    //====== Настраиваемые характеристики ======

    [SerializeField] protected Team _team; public Team team { get => _team; }
    [SerializeField] protected int _maxHealth; public int maxHealth { get => _maxHealth; }
    [SerializeField] protected int _health;
    public int health
    {
        get => _health;
        set
        {
            if (value <= 0) { _health = 0; isDeath = true; onDeath?.Invoke(); Destroy(gameObject); }
            else if (value > _maxHealth) _health = _maxHealth;
            else _health = value;
            onHealthChanged?.Invoke(_health, _maxHealth);
        }
    }


    //====== Свойства ======
    public bool isDeath { get; protected set; }
    public Animator animator { get; protected set; }



    protected virtual void Start() 
    {
        isDeath = false;
    }

    protected virtual void Update() { }



}
