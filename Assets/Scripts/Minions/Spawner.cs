using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Test:")]
    [SerializeField] EnemyCounter enemyCounter;
    [SerializeField] EnemyCounterCommon enemyCounterCommon;


    [Space(30)]
    [SerializeField] private Vector2 _randomPosition;
    [HideInInspector] public float interval;
    [HideInInspector] public int maxHealth;

    [SerializeField] protected SliderNotifier _healthSliderNotifier;
    [SerializeField] protected SliderNotifier _intervalSliderNotifier;

    protected Pool[] _pools;

    private void Start()
    {
        interval = _intervalSliderNotifier.slider.value;
        maxHealth = (int)_healthSliderNotifier.slider.value;
        _pools = GetComponents<Pool>();
        _healthSliderNotifier.onValueChanged += OnValueChanged_HealthSliderNotifier;
        _intervalSliderNotifier.onValueChanged += OnValueChanged_IntervalSliderNotifier;
        StartCoroutine(Spawning());

    }

    private void OnValueChanged_IntervalSliderNotifier(float value)
    {
        interval = value;
    }

    private void OnValueChanged_HealthSliderNotifier(float value)
    {
        maxHealth = (int)value;
    }

    IEnumerator Spawning()
    {
        while (true)
        {
            if (EnemyLimit.instance.Value > enemyCounter.quantity)
            {
                Vector3 position = new Vector3(transform.position.x + Random.Range(-_randomPosition.x, _randomPosition.x),
                transform.position.y + Random.Range(-_randomPosition.y, _randomPosition.y), 0f);

                int randomNumber = Random.Range(0, _pools.Length);
                Minion minion = _pools[randomNumber].GetElement(position, Quaternion.identity).gameObject.GetComponent<Minion>();
                minion.maxHealth = maxHealth;
                minion.health = maxHealth;
                enemyCounterCommon.OnCreate_Minion();
            }

            

            yield return new WaitForSeconds(interval);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(2*_randomPosition.x, 2*_randomPosition.y, 0f));
    }


}

