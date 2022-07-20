using NTC.Global.Cache;
using UnityEngine;

public abstract class Bullet : MonoCache
{
    [HideInInspector] public Team team;
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] protected bool collideInRoadCenter = false;

    protected Animator _animator;
    protected bool _isActive;

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void OnEnabled()
    {
        base.OnEnabled();
        _isActive = true;
    }


    protected override void Run()
    {
        // ѕул€ всегда смотрит в направлении своей скорости
        transform.right = rb.velocity;
    }


    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        // ѕри попадании в преп€тствие пул€ взрываетс€ и деактивируетс€
        if (collider.CompareTag("Obstacle") ||
            (collider.CompareTag("RoadCenter") && collideInRoadCenter))
        {
            _animator.SetTrigger("Collision");
            rb.velocity = Vector3.zero;
            _isActive = false;
        }
    }




}
