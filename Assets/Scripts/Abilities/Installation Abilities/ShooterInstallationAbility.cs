using UnityEngine;

public abstract class ShooterInstallationAbility : MonoBehaviour
{
    public delegate void OnUpdateRechargeTime(float time, float maxTime);
    public delegate void OnEnabling();
    public event OnUpdateRechargeTime onUpdateRechargeTime;
    public event OnEnabling onEnable;
    public event OnEnabling onDisable;

    [SerializeField] protected float _rechargeTime;

    protected bool recharging;


    protected ShooterInstallation _installation;
    private float _timeUntilRecharge;
    protected float timeUntilRecharge { get => _timeUntilRecharge; set { _timeUntilRecharge = value; onUpdateRechargeTime?.Invoke(value, _rechargeTime); } }


    protected virtual void Start()
    {
        _installation = GetComponent<ShooterInstallation>();
        timeUntilRecharge = 0f;
        recharging = true;
    }

    protected virtual void Update()
    {
        if (recharging) timeUntilRecharge -= Time.deltaTime;
    }



    // Переход в режим выполнения способности - отключение обычного режима атаки
    public virtual void Enable()
    {
        _installation.spineAnimation.SetAnimation(AnimationType.Ability_active);
        _installation.enabled = false;
        timeUntilRecharge = _rechargeTime;
        recharging = false;
        onEnable?.Invoke();
    }

    // Переход в обычный режим атаки (обычно в конце анимации)
    public virtual void Disable()
    {
        _installation.enabled = true;
        recharging = true;
        onDisable?.Invoke();
    }

    // Функция, активируемая событием в анимации (или через нажатие кнопки)
    public abstract void Active();


    



}
