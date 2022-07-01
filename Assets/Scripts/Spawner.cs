using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _minion;
    [SerializeField] private Vector2 _randomPosition;
    [HideInInspector] public float interval;
    [HideInInspector] public int maxHealth;

    [SerializeField] protected SliderNotifier _healthSliderNotifier;
    [SerializeField] protected SliderNotifier _intervalSliderNotifier;
    [SerializeField] protected Transform _destination;

    private void Start()
    {
        StartCoroutine(Spawn());
        _healthSliderNotifier.onValueChanged += OnValueChanged_HealthSliderNotifier;
        _intervalSliderNotifier.onValueChanged += OnValueChanged_IntervalSliderNotifier;
        interval = _intervalSliderNotifier.slider.value;
        maxHealth = (int)_healthSliderNotifier.slider.value;
    }

    private void OnValueChanged_IntervalSliderNotifier(float value)
    {
        interval = value;
    }

    private void OnValueChanged_HealthSliderNotifier(float value)
    {
        maxHealth = (int)value;
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            Vector3 position = new Vector3(transform.position.x + Random.Range(-_randomPosition.x, _randomPosition.x),
                transform.position.y + Random.Range(-_randomPosition.y, _randomPosition.y), 0f);
            Minion minion = Instantiate(_minion, position, Quaternion.identity).GetComponent<Minion>();
            minion.maxHealth = maxHealth;
            minion.health = maxHealth;
            minion.destination = _destination;
            yield return new WaitForSeconds(interval);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(2*_randomPosition.x, 2*_randomPosition.y, 0f));
    }


}

