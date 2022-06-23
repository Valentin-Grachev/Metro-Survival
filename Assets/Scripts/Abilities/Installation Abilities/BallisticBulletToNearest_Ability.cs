using UnityEngine;

public class BallisticBulletToNearest_Ability : BallisticBulletToDestination_Ability
{

    public override void Active()
    {
        Transform nearest = Library.SearchNearest(transform.position, _installation.detectionRadius, _installation.enemyLayer);

        // Если нет ближайшего - стреляет в середину
        if (nearest == null)
        {
            Vector2 middle = new Vector2(transform.position.x + (_minDistance + _maxDistance) / 2f, AbilityDestination.instance.aimPosition.y);
            destination = middle;
        }
        else destination = nearest.position;
        base.Active();
    }



}
