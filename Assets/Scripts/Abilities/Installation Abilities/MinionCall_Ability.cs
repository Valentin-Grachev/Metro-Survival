using UnityEngine;

public class MinionCall_Ability : ShooterInstallationAbility
{
    [SerializeField] protected GameObject _minion;
    [SerializeField] protected Transform _spawnPosition;


    public override void Enable()
    {
        timeUntilRecharge = _rechargeTime;
        _installation.animator.SetTrigger("Ability");

        _installation.directionBone.direction = Vector2.right; // ������ �����������
        _installation.enabled = false; // ��������� ���������, ����� ��� �� ������� �������� �����������
    }


    public override void Active()
    {
        Instantiate(_minion, _spawnPosition.position, Quaternion.identity);
    }





}
