using UnityEngine;

public class OneTarget_Bullet : Bullet
{
    [HideInInspector] public int damage;

    protected override void Collide(DestroyableObject collidedObject)
    {
        base.Collide(collidedObject);
        collidedObject.health -= damage;
    }
}
