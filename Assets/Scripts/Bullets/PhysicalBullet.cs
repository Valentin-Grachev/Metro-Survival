using UnityEngine;

public class PhysicalBullet : Bullet
{
    public Rigidbody2D rigidbody;


    protected override void FixedRun() { } // Пуля использует физику а не поиск - фиксированное сканирование не нужно


    protected override void Run()
    {
        base.Run();
        transform.right = rigidbody.velocity.normalized;
    }

    protected override void Collide(DestroyableObject collidedObject)
    {
        base.Collide(collidedObject);
        rigidbody.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle")) Collide(null);
    }






}
