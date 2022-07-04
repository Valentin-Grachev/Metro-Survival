using UnityEngine;

public class Enter_Ability : ShooterInstallationAbility
{
    [SerializeField] protected GameObject _spawnMinion;
    [SerializeField] protected Transform _spawnPosition; // TODO - Сменить на рандомную область
    [SerializeField] protected GameObject _localCanvas;

    public override void Disable()
    {
        base.Disable();
        _localCanvas.SetActive(true);
        timeUntilRecharge = _rechargeTime;
        _installation.animator.SetTrigger("Disable");
    }


    public override void Active()
    {
        Instantiate(_spawnMinion, _spawnPosition.position, Quaternion.identity).
            GetComponent<ReturnableMelee_NavMeshMinion>().ability = this;
    }

    public override void Enable()
    {
        base.Enable();
        _localCanvas.SetActive(false);
    }


}
