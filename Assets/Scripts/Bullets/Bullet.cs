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


    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        // ѕри попадании в преп€тствие пул€ взрываетс€
        if (collider.CompareTag("Obstacle"))
        {
            _animator.SetTrigger("Collision");
            rb.velocity = Vector3.zero;
        }
    }




}
