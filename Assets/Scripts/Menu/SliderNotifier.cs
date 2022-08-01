using UnityEngine;
using UnityEngine.UI;

public class SliderNotifier : MonoBehaviour
{
    public delegate void OnValueChanged(float value);
    public event OnValueChanged onValueChanged;
    private Slider _slider;
    public Slider slider { get => _slider; }


    void Awake() => _slider = GetComponent<Slider>();

    public void Notify() => onValueChanged.Invoke(_slider.value);
}
