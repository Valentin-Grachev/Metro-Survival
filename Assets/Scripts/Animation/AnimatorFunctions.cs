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

    public void RebakeNavMesh() => NavMeshRebaker.instance.Rebake();

    public void DeactiveCollider() => GetComponent<Collider2D>().enabled = false;


}
