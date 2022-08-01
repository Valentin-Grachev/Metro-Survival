using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLimit : MonoBehaviour
{
    public static EnemyLimit instance { get; private set; }

    [SerializeField] private SliderNotifier _sliderNotifier;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;

    public int Value;

    private void Awake()
    {
        instance = this;
        _sliderNotifier.onValueChanged += OnValueChanged_SliderNotifier;
        Value = (int)_slider.value;
    }

    private void OnValueChanged_SliderNotifier(float value)
    {
        _text.text = ((int)value).ToString();
        Value = (int)value;
    }
}
