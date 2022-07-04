using UnityEngine;

public class MinionCall_Ability : ShooterInstallationAbility
{
    [SerializeField] protected GameObject _minion;
    [SerializeField] protected Transform _spawnPosition;


    public override void Enable()
    {
        timeUntilRecharge = _rechargeTime;
        base.Enable();
    }


    public override void Active()
    {
        Instantiate(_minion, _spawnPosition.position, Quaternion.identity);
    }
}
