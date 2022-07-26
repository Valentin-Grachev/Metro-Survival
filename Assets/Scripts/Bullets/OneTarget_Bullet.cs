using UnityEngine;

public class OneTarget_Bullet : Bullet
{
    [HideInInspector] public int damage;

    protected override void Collide(DestroyableObject collidedObject)
    {
        base.Collide(collidedObject);
        if (collidedObject != null) collidedObject.health -= damage;
    }
}
