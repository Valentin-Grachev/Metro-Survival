using System.Collections;
using UnityEngine;

public class AttackGain_Ability : ShooterInstallationAbility
{
    [SerializeField] protected Pool _bulletPool;
    [SerializeField] protected float _duration;
    [SerializeField] protected float _damageGainPercentage;
    [SerializeField] protected float _speedAttackGainPercentage;

    protected float _speedBonus;
    protected int _damageBonus;
    protected Pool _usualBulletPool;

    public override void Enable()
    {
        base.Enable();
        _installation.enabled = true;
        
    }


    public override void Active()
    {
        float gainCoeff = _damageGainPercentage/100f;
        _damageBonus = (int)(_installation.damage * gainCoeff);
        _installation.damage += _damageBonus;

        gainCoeff = _speedAttackGainPercentage / 100f;
        _speedBonus = _installation.attackSpeed * gainCoeff;
        _installation.attackSpeed += _speedBonus;

        _usualBulletPool = _installation.bulletPool;
        _installation.bulletPool = _bulletPool;

        StartCoroutine(DisableWithDelay());

    }

    public override void Disable()
    {
        base.Disable();
        _installation.attackSpeed -= _speedBonus;
        _installation.damage -= _damageBonus;
        _installation.bulletPool = _usualBulletPool;

    }


    IEnumerator DisableWithDelay()
    {
        yield return new WaitForSeconds(_duration);
        Disable();
    }



}
