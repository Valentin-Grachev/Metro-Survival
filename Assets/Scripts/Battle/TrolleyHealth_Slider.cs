using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrolleyHealth_Slider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueText;


    private Slider _slider;

    private void OnEnable()
    {
        if (_slider == null) _slider = GetComponent<Slider>();
        Trolley.instance.onHealthChanged += OnHealthChanged_Trolley;
        OnHealthChanged_Trolley(Trolley.instance.health, Trolley.instance.maxHealth);
    }

    private void OnHealthChanged_Trolley(int health, int maxHealth)
    {
        _slider.value = (float)health/(float)maxHealth;
        _valueText.text = health.ToString();
    }

    private void OnDisable()
    {
        Trolley.instance.onHealthChanged -= OnHealthChanged_Trolley;
    }

}
