using UnityEngine;
using UnityEngine.AI;

public class NavMeshRebaker : MonoBehaviour
{
    public static NavMeshRebaker instance;

    private NavMeshSurface2d _mesh;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        _mesh = GetComponent<NavMeshSurface2d>();
    }

    public void Rebake() => _mesh.BuildNavMesh();


}
