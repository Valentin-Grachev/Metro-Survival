using NTC.Global.Cache;
using UnityEngine;

public abstract class Bullet : MonoCache
{
    [HideInInspector] public Team teamForCollide;
    [SerializeField] protected bool collideInRoadCenter = false;

    protected SpineAnimation _spineAnimation;
    protected bool _isActive;
    protected Vector2 _velocity;
    public Vector2 velocity { get => _velocity; set { _velocity = value; transform.right = velocity.normalized; } }

    protected virtual void Start()
    {
        _spineAnimation = GetComponent<SpineAnimation>();
    }

    protected override void OnEnabled()
    {
        base.OnEnabled();
        _isActive = true;
    }

    protected override void Run()
    {
        base.Run();
        if (!_isActive) return;
        transform.Translate(velocity * Time.deltaTime);
        if (Library.IsCollided(transform.position, teamForCollide, out DestroyableObject collided))
            Collide(collided);
        
    }

    protected virtual void Collide(DestroyableObject collidedObject)
    {
        _isActive = false;
        _spineAnimation.SetAnimation(AnimationType.Death, true);
    }





}
