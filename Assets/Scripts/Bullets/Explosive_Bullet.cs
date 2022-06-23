using UnityEngine;

public class Explosive_Bullet : Bullet
{
    [SerializeField] protected GameObject _explotion;
    [SerializeField] protected float _explotionScale = 1f;

    [HideInInspector] public int damage;
    [HideInInspector] public int damageRadius;


    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        // Попали в коллайдер миньона - пуля летит дальше
        if (collider.transform.parent.TryGetComponent(out Minion minion)) return;

        // Взрыв при попадании в землю
        _animator.SetTrigger("Collision");
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        if (_explotion != null) 
            Instantiate(_explotion, transform.position, Quaternion.identity).transform.localScale *= _explotionScale * damageRadius;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius, 1 << LayerMask.NameToLayer("BulletCollider"));
        foreach (var item in colliders)
        {
            if (item.transform.parent.TryGetComponent(out DestroyableObject destrObject) && destrObject.team != team)
            {
                destrObject.health -= damage;
            }
            
        }
    }


    



}
