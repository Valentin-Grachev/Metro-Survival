using UnityEngine;

public class MinionCall_Ability : ShooterInstallationAbility
{
    [SerializeField] protected GameObject _minion;


    public override void Enable()
    {
        timeUntilRecharge = _rechargeTime;
        _installation.spineAnimation.SetAnimation(AnimationType.Ability_active);

        _installation.directionBone.direction = Vector2.right; // ������ �����������
        _installation.enabled = false; // ��������� ���������, ����� ��� �� ������� �������� �����������
    }


    public override void Active()
    {
        Instantiate(_minion, Trolley.instance.transform.position, Quaternion.identity);
    }





}
