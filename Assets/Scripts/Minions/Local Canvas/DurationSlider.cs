using UnityEngine;
using UnityEngine.UI;

public class DurationSlider : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        GetComponentInParent<TempDestroybleObject>().onUpdateTimeUntilDestroy += OnUpdateTimeUntilDestroy_TempDestroybleObject;
    }

    private void OnUpdateTimeUntilDestroy_TempDestroybleObject(float time, float maxTime)
    {
        _slider.value = time / maxTime;
    }
}
