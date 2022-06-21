using UnityEngine;

public class LinearOneTargetBullet : Bullet
{
    [HideInInspector] public int damage;

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        // Попали во вражеского миньона
        if (collider.transform.parent.TryGetComponent(out Minion minion))
        {
            if (minion.team != team) minion.health -= damage;
        }
        // Взрыв при любом попадании в коллайдер для пуль
        _animator.SetTrigger("Collision");
        rb.velocity = Vector3.zero;
    }
}
