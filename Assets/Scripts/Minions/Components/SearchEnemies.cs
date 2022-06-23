using UnityEngine;

public class SearchEnemies : MonoBehaviour
{
    [SerializeField] private float _radius;

    private Minion _minion;
    private float _timeUntilNewScan = scanInterval;
    private const float scanInterval = 0.5f;

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
            _timeUntilNewScan = scanInterval;

            // Поиск новой ближайшей цели
            if (_minion.destination == null)
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _radius, 1 << LayerMask.NameToLayer("Enemies"));

                float minDistance = 10000f;
                foreach (var item in enemies)
                {
                    if (Vector2.Distance(transform.position, item.transform.position) < minDistance)
                    {
                        minDistance = Vector2.Distance(transform.position, item.transform.position);
                        _minion.destination = item.transform;
                        
                    }
                }
                    
            }

        }

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }


}
