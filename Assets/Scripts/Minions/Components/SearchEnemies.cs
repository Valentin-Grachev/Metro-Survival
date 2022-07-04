using UnityEngine;

public class SearchEnemies : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _detectionLayer;

    private Minion _minion;
    private float _timeUntilNewScan = Constants.scanInterval;

    private void Start()
    {
        _minion = GetComponent<Minion>();
    }

    // Функция обновления, основной код которой выполняется через определенный константой интервал
    private void Update()
    {

        _timeUntilNewScan -= Time.deltaTime;
        if (_timeUntilNewScan <= 0)
        {
            _timeUntilNewScan = Constants.scanInterval;

            // Поиск новой ближайшей цели
            if (_minion.destination == null)
            {
                Transform newDestination = Library.SearchNearestCircle(transform.position, _radius, _detectionLayer);
                if (newDestination != null) _minion.destination = newDestination;
            }
                    

        }

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }


}
