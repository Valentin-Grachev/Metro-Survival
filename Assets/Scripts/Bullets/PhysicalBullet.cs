using UnityEngine;

public class PhysicalBullet : Bullet
{
    [HideInInspector] public Rigidbody2D rigidbody;


    protected override void OnEnabled()
    {
        base.OnEnabled();
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = false;
    }

    protected override void Run()
    {
        base.Run();
        transform.right = rigidbody.velocity.normalized;
    }

    protected override void Collide(DestroyableObject collidedObject)
    {
        base.Collide(collidedObject);
        rigidbody.velocity = Vector2.zero;
        rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle")) Collide(null);
    }






}
