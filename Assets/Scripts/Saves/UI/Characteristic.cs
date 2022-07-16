using TMPro;
using UnityEngine;

public class Characteristic : MonoBehaviour
{
    private enum CharacteristicType { Health, HeroSlots, ModuleSlot, Level, Price }


    [SerializeField] private string _id;
    [SerializeField] private CharacteristicType _type;
    [SerializeField] private bool showNext;

    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        PlayerProgress.instance.onUpdateValue += OnChangeValue_PlayerProgress;
        if (_id == "money") PlayerProgress.instance.onUpdateMoney += OnChangeMoney_PlayerProgress;
    }

    private void OnChangeMoney_PlayerProgress()
    {
        _text.text = PlayerProgress.instance.money.ToString();
    }

    private void OnChangeValue_PlayerProgress()
    {

        if (_id == "trolley")
        {
            int showLevel = PlayerProgress.instance.trolleyLevel;
            if (showNext) showLevel++;
            switch (_type)
            {
                case CharacteristicType.Health:
                    _text.text = PlayerProgress.instance.trolleyLevelsSO.levels[showLevel].health.ToString();
                    break;
                case CharacteristicType.HeroSlots:
                    _text.text = PlayerProgress.instance.trolleyLevelsSO.levels[showLevel].heroSlots.ToString();
                    break;
                case CharacteristicType.ModuleSlot:
                    _text.text = PlayerProgress.instance.trolleyLevelsSO.levels[showLevel].moduleSlots.ToString();
                    break;
                case CharacteristicType.Level:
                    _text.text = PlayerProgress.instance.trolleyLevel.ToString();
                    break;
                case CharacteristicType.Price:
                    _text.text = PlayerProgress.instance.trolleyLevelsSO.levels[showLevel].price.ToString();
                    break;
            }
        }
    }
}
