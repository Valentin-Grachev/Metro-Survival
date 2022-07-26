using UnityEngine;

public abstract class ControlledAbility : ShooterInstallationAbility
{
    [Header("Control Parameters:")]
    [SerializeField] protected float _minDistance;
    [SerializeField] protected float _maxDistance;
    [SerializeField] protected GameObject _aim;


    protected Vector2 _destination;
    protected Vector2 destination
    {
        // Ограничение по дальности точки назначения
        get => _destination;
        set
        {
            Vector2 newDestination = new Vector2(value.x, value.y);
            if (newDestination.x < transform.position.x + _minDistance) newDestination.x = transform.position.x + _minDistance;
            else if (newDestination.x > transform.position.x + _maxDistance) newDestination.x = transform.position.x + _maxDistance;
            _destination = newDestination;
        }
    }


    // Подписка на событие (функция вызывается через нажатие кнопки)
    public void EnableControl()
    {
        AbilityDestination.instance.onDestination += OnDestination_AbilityDestination;
        AbilityDestination.instance.maxDistance = _maxDistance;
        AbilityDestination.instance.minDistance = _minDistance;
        AbilityDestination.instance.startPosition = transform.position;
        AbilityDestination.instance.aim = _aim;
        AbilityDestination.instance.enabled = true;
    }

    private void OnDestination_AbilityDestination(Vector2 destination)
    {
        Enable();
        this.destination = destination;
        
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 from = new Vector2(transform.position.x + _minDistance, transform.position.y - 1f);
        Vector2 to = new Vector2(transform.position.x + _maxDistance, transform.position.y - 1f);

        Gizmos.DrawLine(from, to);
    }




}
