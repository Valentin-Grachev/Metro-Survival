using UnityEngine;

public class Mine_Ability : ControlledAbility
{
    [Header("Common Parameters:")]
    [SerializeField] protected GameObject _mine;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _activationRadius;
    [SerializeField] protected float _damageRadius;
    [SerializeField] protected LayerMask _damageLayer;


    public override void Active()
    {
        timeUntilRecharge = _rechargeTime;
        Mine mine = Instantiate(_mine, destination + (Vector2)_mine.transform.position, Quaternion.identity).GetComponent<Mine>();

        mine.damage = _damage;
        mine.activationRadius = _activationRadius;
        mine.damageRadius = _damageRadius;
        mine.damageLayer = _damageLayer;



        NavMeshRebaker.instance.Rebake();
    }

    public override void Enable() => Active();

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(-2f, 2f, 0f), _damageRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + new Vector3(-2f, 2f, 0f), _activationRadius);
    }



}
