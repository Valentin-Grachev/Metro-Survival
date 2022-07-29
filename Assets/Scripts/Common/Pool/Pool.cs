using System.Collections.Generic;
using UnityEngine;


// Авторасширяемый пул объектов
public class Pool : MonoBehaviour
{
    [SerializeField] private PoolObject _prefab;
    [SerializeField] private int _startCapacity;

    private List<PoolObject> _pool;


    private void Start()
    {
        if (_pool == null) CreatePool();
    }

    private void CreatePool()
    {
        _pool = new List<PoolObject>(_startCapacity);
        for (int i = 0; i < _startCapacity; i++) CreateElement();
    }


    private PoolObject CreateElement(bool isActive = false)
    {
        PoolObject createdObject = Instantiate(_prefab, PoolContainer.container.transform);
        createdObject.gameObject.SetActive(isActive);
        _pool.Add(createdObject);
        return createdObject;
    }


    private bool TryGetElement(out PoolObject element)
    {
        if (_pool == null) CreatePool();

        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }


    public PoolObject GetElement(Vector3 position, Quaternion rotation)
    {
        PoolObject result;
        if (TryGetElement(out var element)) result = element;
        else result = CreateElement(true);

        result.transform.position = position;
        result.transform.rotation = rotation;
        return result;
    }



}
