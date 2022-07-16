using CI.QuickSave;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public delegate void OnUpdateValue();
    public event OnUpdateValue onUpdateValue;
    public event OnUpdateValue onUpdateMoney;

    public static PlayerProgress instance { get; private set; }
    private QuickSaveSettings _settings;
    private void Awake()
    {
        if (instance == null) instance = this;
        _settings = new QuickSaveSettings
        {
            SecurityMode = SecurityMode.Aes,
            Password = Constants.savePassword,
            CompressionMode = CompressionMode.Gzip
        };

    }


    



    [Header("Sriptable Objects:")]
    [SerializeField] private TrolleyLevelsSO _trolleyLevelsSO;
    public TrolleyLevelsSO trolleyLevelsSO { get => _trolleyLevelsSO; }




    private int _money; public int money { get => _money; set { _money = value; onUpdateMoney?.Invoke(); } }
    private int _premiumMoney; public int premiumMoney { get => _premiumMoney; set { _premiumMoney = value; onUpdateMoney?.Invoke(); } }

    private int _trolleyLevel;
    public int trolleyLevel
    {
        get => _trolleyLevel;
        private set // Установка нового уровня тележки
        {
            // Удаляем старый префаб
            Vector3 trolleyPosition = Trolley.instance.transform.position;
            Destroy(Trolley.instance.gameObject);

            // Создаем новый префаб если уровень достаточен для изменения внешнего вида тележки
            if (Trolley.instance.gameObject != _trolleyLevelsSO.levels[value].trolleyPrefab)
                Instantiate(_trolleyLevelsSO.levels[value].trolleyPrefab, trolleyPosition, Quaternion.identity);
            Trolley.instance.maxHealth = _trolleyLevelsSO.levels[value].health;
            Trolley.instance.health = _trolleyLevelsSO.levels[value].health;


            /* TODO

            // Расставляем модули и стрелков
            for (int i = 0; i < heroPositions.Length; i++)
            {

            }

            */

            _trolleyLevel = value;
            onUpdateValue?.Invoke();
        }
    }


    public List<string> heroesId; // id героя на i-той позиции
    public Dictionary<string, int> heroLevels;


    private void Start()
    {
        trolleyLevel = DefaultValue.trolleyLevel;
        money = DefaultValue.money;
    }







    public void UpLevel(string id)
    {
        if (id == "trolley" && money >= _trolleyLevelsSO.levels[trolleyLevel+1].price)
        {
            money -= _trolleyLevelsSO.levels[trolleyLevel+1].price;
            trolleyLevel++;
        }
    }










    public void Save()
    {
        var writer = QuickSaveWriter.Create(Constants.saveRoot, _settings);
        writer.Write("money", money)
            .Write("premium_money", premiumMoney)
            .Write("trolley_level", trolleyLevel);
        for (int i = 0; i < heroesId.Count; i++)
            writer.Write("hero_position_" + i.ToString(), heroesId[i]);

        foreach (var item in heroLevels)
            writer.Write(item.Key, item.Value);


        writer.Commit();
    }

    public void Load()
    {
        var reader = QuickSaveReader.Create(Constants.saveRoot, _settings);
        //if (reader.Exists(_key)) _text.text = reader.Read<string>(_key);


    }






}
