using UnityEngine;

public class BallisticBulletToNearest_Ability : BallisticBulletToDestination_Ability
{
    protected DestroyableObject _nearestTarget;
    protected Vector2 _lastNearestPosition;

    


    public override void Active()
    {
        
        if (_nearestTarget != null && !_nearestTarget.isDeath)
        {
            // Берем с упреждением - переопределяем снова точку назначения
            float velocityX = 0f;
            if (_nearestTarget is Minion) velocityX = ((Minion)_nearestTarget).velocityX;
            Vector2 nearestPosition = new Vector2(_nearestTarget.transform.position.x, AbilityDestination.instance.aimPosition.y);

            // Стреляем оптимально - за первого ближнего так, чтобы его задело и задних накрыло
            destination = nearestPosition + new Vector2(velocityX*Constants.prediction, 0f) + new Vector2(_damageArea.x * Constants.backOffset, 0f);
        }
        else
        {
            destination = _lastNearestPosition + new Vector2(_damageArea.x * Constants.backOffset, 0f);
        }
        
        
        base.Active();
    }


    public override void Enable()
    {
        // Если не находим цель - ничего не делаем
        if (Library.TryFindNearestUntilLine(transform.position,
            BulletLimiter.instance.transform.position.x + BulletLimiter.instance.detectionLine, Team.Enemy, out _nearestTarget)
            == false)
            return;


        _lastNearestPosition = _nearestTarget.transform.position;

        // Устанавливаем предварительную точку назначения, для установки угла выстрела
        destination = _nearestTarget.transform.position;

        base.Enable();
    }



}
