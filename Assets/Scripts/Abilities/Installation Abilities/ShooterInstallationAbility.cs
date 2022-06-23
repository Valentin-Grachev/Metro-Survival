using UnityEngine;

public abstract class ShooterInstallationAbility : MonoBehaviour
{
    [SerializeField] protected float _rechargeTime;



    protected ShooterInstallation _installation;
    protected float _timeUntilRecharge;



    protected virtual void Start()
    {
        _installation = GetComponent<ShooterInstallation>();
        _timeUntilRecharge = _rechargeTime;
    }

    protected virtual void Update()
    {
        _timeUntilRecharge -= Time.deltaTime;
    }



    // ������� � ����� ���������� ����������� - ���������� �������� ������ �����
    public virtual void Enable()
    {
        _installation.animator.SetTrigger("Ability");
        _installation.enabled = false;
    }

    // ������� � ������� ����� ����� (������ � ����� ��������)
    public virtual void Disable()
    {
        _installation.enabled = true;
    }

    // �������, ������������ �������� � �������� (��� ����� ������� ������)
    public abstract void Active();


    



}
