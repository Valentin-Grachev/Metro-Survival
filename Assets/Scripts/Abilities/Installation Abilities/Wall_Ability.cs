using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Ability : ControlledAbility
{
    [Header("Common Parameters:")]
    [SerializeField] protected GameObject _wall;
    [SerializeField] protected float _duration;


    public override void Active()
    {
        TempDestroybleObject wall = Instantiate(_wall, destination + (Vector2)_wall.transform.position, Quaternion.identity).GetComponent<TempDestroybleObject>();
        wall.timeUntilDestroy = _duration;
        wall.maxTimeUntilDestroy = _duration;
    }

    public override void Enable() => Active();


}
