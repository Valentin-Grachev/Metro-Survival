using NTC.Global.Cache;

// Класс для объктов, которые инициализируются из пула - при первом создании работает стандартная функция Start,
// затем FromPool при последующем включении объекта
public class PoolBehaviour : MonoCache
{
    private bool _fromPool = false;



    protected virtual void Start()
    {
        // Если это не пуловый объект, FromPool выполняем сразу в методе Start
        if (!TryGetComponent(out PoolObject pool)) FromPool();
    }

    protected override void OnEnabled()
    {
        base.OnEnabled();
        

        // FromPool выполняем не сразу, а после создания и деактивации
        if (_fromPool) FromPool();
    }

    protected override void OnDisabled()
    {
        base.OnDisabled();
        _fromPool = true;
    }


    // Нужно в классе переопределять этот метод при возврате из пула
    protected virtual void FromPool(){}



}
