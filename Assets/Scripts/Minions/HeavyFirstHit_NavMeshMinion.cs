using System.Collections.Generic;
using UnityEngine;

public class HeavyFirstHit_NavMeshMinion : OneMeleeTarget_NavMeshMinion
{
    [Header("Heavy-First-Hit Parameters:")]
    [SerializeField] protected int _damageFirstHit;
    [SerializeField] protected Vector2 _damageAreaSize;
    [SerializeField] protected Vector2 _damageAreaOffset;
    protected bool _hasAbility;



    public override DestroyableObject attackedTarget
    { 
        get => base.attackedTarget;
        protected set
        {
            if (value != null && _hasAbility)
            {
                // Первый удар - особый
                _hasAbility = false;
                spineAnimation.SetAnimation(AnimationType.Ability_active);
                _attackedTarget = value;
            }
            else base.attackedTarget = value;
        }
    }


    protected override void Start()
    {
        base.Start();
        _hasAbility = true;
    }


    public void HeavyFirstHit()
    {
        if (Library.TryFindInsideAreaAll((Vector2)transform.position + _damageAreaOffset, _damageAreaSize, enemyTeam, out List<DestroyableObject> founded))
            for (int i = 0; i < founded.Count; i++)
                founded[i].health -= _damageFirstHit;

    }


    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + _damageAreaOffset, _damageAreaSize);
    }

}
