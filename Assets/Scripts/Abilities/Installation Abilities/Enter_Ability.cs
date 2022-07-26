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
        _installation.spineAnimation.SetAnimation(AnimationType.Ability_deactive);
    }


    public override void Active()
    {
        Vector3 instPosition = Trolley.instance.spawnPosition.position;
        instPosition += new Vector3(0f, Random.Range(-Constants.randomizationPosition, Constants.randomizationPosition), 0f);

        Instantiate(_spawnMinion, instPosition, Quaternion.identity).
            GetComponent<ReturnableMelee_NavMeshMinion>().ability = this;
    }

    public override void Enable()
    {
        base.Enable();
        _localCanvas.SetActive(false);
    }


}
