using UnityEngine;

public abstract class ShooterInstallationAbility : MonoBehaviour
{
    [SerializeField] protected float _rechargeTime;



    protected ShooterInstallation _installation;
    protected float _timeUntilRecharge;



    protected virtual void Start()
    {
        _installation = GetComponent<ShooterInstallation>();
        _timeUntilRecharge = _rechargeTime;
    }

    protected virtual void Update()
    {
        _timeUntilRecharge -= Time.deltaTime;
    }



    // Переход в режим выполнения способности - отключение обычного режима атаки
    public virtual void Enable()
    {
        _installation.animator.SetTrigger("Ability");
        _installation.enabled = false;
    }

    // Переход в обычный режим атаки (обычно в конце анимации)
    public virtual void Disable()
    {
        _installation.enabled = true;
    }

    // Функция, активируемая событием в анимации (или через нажатие кнопки)
    public abstract void Active();


    



}
