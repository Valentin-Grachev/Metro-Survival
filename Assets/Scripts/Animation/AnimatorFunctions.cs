using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
    private PoolObject _poolObject;

    private void Start()
    {
        TryGetComponent(out _poolObject);
    }



    public void ReturnToPool() => _poolObject.ReturnToPool();

    public void Destroy() => Destroy(gameObject);

}
