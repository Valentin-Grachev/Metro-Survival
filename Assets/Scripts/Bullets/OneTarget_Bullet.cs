using UnityEngine;

public class OneTarget_Bullet : Bullet
{
    [HideInInspector] public int damage;

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        // Попали во вражеского миньона
        if (collider.transform.parent.TryGetComponent(out Minion minion) && minion.team != team)
        {
            minion.health -= damage;
            _animator.SetTrigger("Collision");
            rb.velocity = Vector3.zero;
        }
    }
}
