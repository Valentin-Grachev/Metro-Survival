using UnityEngine;

public class SearchEnemies : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _detectionLayer;

    private Minion _minion;

    private void Start()
    {
        _minion = GetComponent<Minion>();
    }

    private void Update()
    {
         // Поиск новой ближайшей цели
        if (_minion.destination == null)
        {
            Transform newDestination = Library.SearchNearestCircle(transform.position, _radius, _detectionLayer);
            if (newDestination != null) _minion.destination = newDestination;
        }

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }


}
