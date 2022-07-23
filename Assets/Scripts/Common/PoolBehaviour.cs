using NTC.Global.Cache;

// ����� ��� �������, ������� ���������������� �� ���� - ��� ������ �������� �������� ����������� ������� Start,
// ����� FromPool ��� ����������� ��������� �������
public class PoolBehaviour : MonoCache
{
    private bool _fromPool = false;



    protected virtual void Start()
    {
        // ���� ��� �� ������� ������, FromPool ��������� ����� � ������ Start
        if (!TryGetComponent(out PoolObject pool)) FromPool();
    }

    protected override void OnEnabled()
    {
        base.OnEnabled();
        

        // FromPool ��������� �� �����, � ����� �������� � �����������
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
