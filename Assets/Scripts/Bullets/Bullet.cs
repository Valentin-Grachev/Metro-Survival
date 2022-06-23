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

    protected virtual void Update()
    {
        // ѕул€ всегда смотрит в направлении своей скорости
        transform.right = rb.velocity;
    }


    protected abstract void OnTriggerEnter2D(Collider2D collider);




}
