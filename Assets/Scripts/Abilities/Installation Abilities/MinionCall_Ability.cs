using UnityEngine;

public class MinionCall_Ability : ShooterInstallationAbility
{
    [SerializeField] protected GameObject _minion;
    [SerializeField] protected Transform _spawnPosition;


    public override void Enable()
    {
        timeUntilRecharge = _rechargeTime;
        _installation.animator.SetTrigger("Ability");

        _installation.directionBone.direction = Vector2.right; // Задаем направление
        _installation.enabled = false; // Выключаем установку, чтобы она не портила заданное направление
    }


    public override void Active()
    {
        Instantiate(_minion, _spawnPosition.position, Quaternion.identity);
    }





}
