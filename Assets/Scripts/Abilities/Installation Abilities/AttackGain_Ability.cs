using System.Collections;
using UnityEngine;

public class AttackGain_Ability : ShooterInstallationAbility
{
    [SerializeField] protected GameObject _bullet;
    [SerializeField] protected float _duration;
    [SerializeField] protected float _damageGainPercentage;
    [SerializeField] protected float _speedAttackGainPercentage;

    protected float _speedBonus;
    protected int _damageBonus;
    protected GameObject _usualBullet;

    public override void Enable()
    {
        _installation.animator.SetTrigger("Ability");
        timeUntilRecharge = _rechargeTime;
        Active();
        StartCoroutine(DisableWithDelay());
    }


    public override void Active()
    {
        float gainCoeff = _damageGainPercentage/100f;
        _damageBonus = (int)(_installation.damage * gainCoeff);
        _installation.damage += _damageBonus;

        gainCoeff = _speedAttackGainPercentage / 100f;
        _speedBonus = _installation.attackSpeed * gainCoeff;
        _installation.attackSpeed += _speedBonus;

        _usualBullet = _installation.bullet;
        _installation.bullet = _bullet;

    }

    public override void Disable()
    {
        _installation.attackSpeed -= _speedBonus;
        _installation.damage -= _damageBonus;
        _installation.animator.SetTrigger("DisableAbility");
        _installation.bullet = _usualBullet;

    }


    IEnumerator DisableWithDelay()
    {
        yield return new WaitForSeconds(_duration);
        Disable();
    }



}
