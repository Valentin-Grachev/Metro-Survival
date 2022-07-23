using UnityEngine;

public abstract class ShooterInstallationAbility : MonoBehaviour
{
    public delegate void OnUpdateRechargeTime(float time, float maxTime);
    public event OnUpdateRechargeTime onUpdateRechargeTime;

    [SerializeField] protected float _rechargeTime;
    



    protected ShooterInstallation _installation;
    private float _timeUntilRecharge;
    protected float timeUntilRecharge { get => _timeUntilRecharge; set { _timeUntilRecharge = value; onUpdateRechargeTime?.Invoke(value, _rechargeTime); } }


    protected virtual void Start()
    {
        _installation = GetComponent<ShooterInstallation>();
        timeUntilRecharge = 0f;
    }

    protected virtual void Update()
    {
        timeUntilRecharge -= Time.deltaTime;
    }



    // Переход в режим выполнения способности - отключение обычного режима атаки
    public virtual void Enable()
    {
        _installation.spineAnimation.SetAnimation(AnimationType.Ability_active);
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
