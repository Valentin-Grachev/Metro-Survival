using UnityEngine;

public class Mine_Ability : ControlledAbility
{
    [Header("Common Parameters:")]
    [SerializeField] protected GameObject _mine;
    [SerializeField] protected int _damage;
    [SerializeField] protected Vector2 _activationAreaSize;
    [SerializeField] protected Vector2 _damageAreaSize;


    public override void Active()
    {
        timeUntilRecharge = _rechargeTime;
        Mine mine = Instantiate(_mine, destination + (Vector2)_mine.transform.position, Quaternion.identity).GetComponent<Mine>();

        mine.damage = _damage;
        mine.activationAreaSize = _activationAreaSize;
        mine.damageAreaSize = _damageAreaSize;
        mine.damagableTeam = Team.Enemy;


    }


    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _damageAreaSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, _activationAreaSize);
    }



}
