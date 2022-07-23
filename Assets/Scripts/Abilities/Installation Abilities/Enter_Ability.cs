using UnityEngine;

public class Enter_Ability : ShooterInstallationAbility
{
    [SerializeField] protected GameObject _spawnMinion;
    [SerializeField] protected GameObject _localCanvas;

    public override void Disable()
    {
        base.Disable();
        _localCanvas.SetActive(true);
        timeUntilRecharge = _rechargeTime;
        //_installation.animator.SetTrigger("Disable");
    }


    public override void Active()
    {
        Instantiate(_spawnMinion, Trolley.instance.transform.position, Quaternion.identity).
            GetComponent<ReturnableMelee_NavMeshMinion>().ability = this;
    }

    public override void Enable()
    {
        base.Enable();
        _localCanvas.SetActive(false);
    }


}
