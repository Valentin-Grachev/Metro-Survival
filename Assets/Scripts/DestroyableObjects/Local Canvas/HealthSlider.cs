using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        GetComponentInParent<Minion>().onHealthChanged += OnHealthChanged_Minion;
    }

    private void OnHealthChanged_Minion(int health, int maxHealth)
    {
        _slider.value = (float)health / (float)maxHealth;
    }


}
