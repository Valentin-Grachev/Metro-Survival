using UnityEngine;

public class Armor_Module : MonoBehaviour
{
    [Tooltip("На данное количество процентов увеличится запас прочности тележки.")]
    [SerializeField] protected float _bonusHealthPercentage;
    public float bonusHealthPercentage
    {
        get => _bonusHealthPercentage;
        set
        {
            _bonusHealthPercentage = value;
            Trolley.instance.maxHealth -= _bonusHealth;
            _bonusHealth = (int)(Trolley.instance.maxHealth * _bonusHealthPercentage / 100f);
            Trolley.instance.maxHealth += _bonusHealth;
            Trolley.instance.health = Trolley.instance.maxHealth;
        }
    }

    protected int _bonusHealth = 0;

    private void Start()
    {
        bonusHealthPercentage = bonusHealthPercentage;
        
    }



}
