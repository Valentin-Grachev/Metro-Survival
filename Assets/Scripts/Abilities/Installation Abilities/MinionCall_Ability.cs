using UnityEngine;

public class MinionCall_Ability : ShooterInstallationAbility
{
    [SerializeField] protected GameObject _minion;

    


    public override void Enable()
    {
        timeUntilRecharge = _rechargeTime;
        _installation.spineAnimation.SetAnimation(AnimationType.Ability_active);

        _installation.directionBone.direction = Vector2.right; // Задаем направление
        _installation.enabled = false; // Выключаем установку, чтобы она не портила заданное направление
    }


    public override void Active()
    {
        Vector3 instPosition = Trolley.instance.spawnPosition.position;
        instPosition += new Vector3(0f, Random.Range(-Constants.randomizationPosition, Constants.randomizationPosition), 0f);
        Instantiate(_minion, instPosition, Quaternion.identity);
    }





}
