using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    private Button _button;
    private Image _image;


    private void Start()
    {
        GetComponentInParent<ShooterInstallationAbility>().onUpdateRechargeTime += OnUpdateRechargeTime_ShooterInstallationAbility;
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    private void OnUpdateRechargeTime_ShooterInstallationAbility(float time, float maxTime)
    {
        if (time <= 0f)
        {
            _image.fillAmount = 1f; 
            _button.interactable = true;
        }
        else
        {
            _image.fillAmount = (maxTime - time) / maxTime;
            _button.interactable = false;
        }
        
    }
}
