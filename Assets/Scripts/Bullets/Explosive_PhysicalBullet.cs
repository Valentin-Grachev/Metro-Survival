using System.Collections.Generic;
using UnityEngine;

public class Explosive_PhysicalBullet : PhysicalBullet
{

    [HideInInspector] public int damage;
    [HideInInspector] public Vector2 damageArea;


    protected override void Collide(DestroyableObject collidedObject)
    {
        base.Collide(collidedObject);
        Library.TryFindInsideAreaAll(transform.position, damageArea, teamForCollide, out List<DestroyableObject> objects);
        for (int i = 0; i < objects.Count; i++) objects[i].health -= damage;
        CameraControl.instance.ShakeDown();

    }






}
