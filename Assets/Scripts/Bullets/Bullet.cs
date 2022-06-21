using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [HideInInspector] public Team team;
    [HideInInspector] public Rigidbody2D rb;

    protected Animator _animator;

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
    }


    protected abstract void OnTriggerEnter2D(Collider2D collider);




}
