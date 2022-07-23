using UnityEngine;

public class Wall_Ability : ControlledAbility
{
    [Header("Common Parameters:")]
    [SerializeField] protected GameObject _wall;
    [SerializeField] protected float _duration;


    public override void Active()
    {
        timeUntilRecharge = _rechargeTime;
        TempDestroybleObject wall = Instantiate(_wall, destination + (Vector2)_wall.transform.position, Quaternion.identity).GetComponent<TempDestroybleObject>();
        wall.timeUntilDestroy = _duration;
        wall.maxTimeUntilDestroy = _duration;
        NavMeshRebaker.instance.Rebake();
    }

    public override void Enable() => _installation.spineAnimation.SetAnimation(AnimationType.Ability_active);


}
