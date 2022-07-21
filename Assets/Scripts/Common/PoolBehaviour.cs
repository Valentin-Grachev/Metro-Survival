using NTC.Global.Cache;

// ����� ��� �������, ������� ���������������� �� ���� - ��� ������ �������� �������� ����������� ������� Start,
// ����� FromPool ��� ����������� ��������� �������
public class PoolBehaviour : MonoCache
{
    private bool _fromPool = false;

    protected override void OnEnabled()
    {
        base.OnEnabled();
        if (_fromPool) FromPool();
    }

    protected override void OnDisabled()
    {
        base.OnDisabled();
        _fromPool = true;
    }


    // ����� � ������ �������������� ���� ����� ��� �������� �� ����
    protected virtual void FromPool(){}



}
