using NTC.Global.Cache;
using UnityEngine;

public abstract class Bullet : MonoCache
{
    [HideInInspector] public Team teamForCollide;

    [Tooltip("Размер пули во время взрыва")]
    [SerializeField] protected Vector2 _deathSize;

    protected SpineAnimation _spineAnimation;
    protected bool _isActive;
    protected Vector2 _velocity;
    protected Vector2 _startSize;
    public Vector2 velocity { get => _velocity; set { _velocity = value; transform.right = velocity.normalized; } }

    protected virtual void Start()
    {
        _spineAnimation = GetComponent<SpineAnimation>();
        _startSize = transform.localScale;
    }

    protected override void OnEnabled()
    {
        base.OnEnabled();
        _isActive = true;
        if (_startSize != Vector2.zero) transform.localScale = _startSize;
    }




    protected override void Run()
    {
        base.Run();
        if (!_isActive) return;


        transform.Translate(velocity * Time.deltaTime, Space.World);
        if (Library.IsCollided(transform.position, teamForCollide, out DestroyableObject collided) && this is not PhysicalBullet)
            Collide(collided);


        if (BulletLimiter.instance.ObjectIsInsideArea(transform.position) == false) Collide(null);

    }

    protected virtual void Collide(DestroyableObject collidedObject)
    {
        _isActive = false;
        _spineAnimation.SetAnimation(AnimationType.Death);
        transform.localScale = new Vector3(_deathSize.x, _deathSize.y, transform.localScale.z);
    }


    public void Destroy() => Destroy(gameObject);


}
