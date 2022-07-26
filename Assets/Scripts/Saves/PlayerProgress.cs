using CI.QuickSave;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

        // ��������� �������� ����������
        _settings = new QuickSaveSettings
        {
            SecurityMode = SecurityMode.Aes,
            Password = Constants.savePassword, // TODO - �������� �� ��������� ���������
            CompressionMode = CompressionMode.Gzip
        };

    }


    



    [Header("Sriptable Objects:")]
    [SerializeField] private TrolleyLevelsSO _trolleyLevelsSO;
    public TrolleyLevelsSO trolleyLevelsSO { get => _trolleyLevelsSO; }




    private int _money; public int money { get => _money; set { _money = value; onUpdateMoney?.Invoke(); } }
    private int _premiumMoney; public int premiumMoney { get => _premiumMoney; set { _premiumMoney = value; onUpdateMoney?.Invoke(); } }
    private int _trolleyLevel;
    


    public List<string> heroesId; // id ����� �� i-��� �������
    public Dictionary<string, int> heroLevels;


    private void Start()
    {
        trolleyLevel = DefaultValue.trolleyLevel;
        money = DefaultValue.money;
        heroesId = DefaultValue.heroesId;
        Trolley.instance.UpdateInstallations();

    }



    // ��������� ������ ������ �������
    public int trolleyLevel
    {
        get => _trolleyLevel;
        private set 
        {
            // ���� ������� ��� ������� �� �������� ������� - ������� ������ � ������� ����� � ������������ ��������
            int currentView = _trolleyLevelsSO.GetViewNumber(_trolleyLevel);
            int newView = _trolleyLevelsSO.GetViewNumber(value);
            if (currentView != newView)
            {
                Vector3 trolleyPosition = Trolley.instance.transform.parent.position;

                // ������� ������ ������
                Destroy(Trolley.instance.gameObject);

                // ������� ����� ������ ������� � ��������� ��������
                Instantiate(_trolleyLevelsSO.GetPrefab(value), trolleyPosition, Quaternion.identity);
                Trolley.instance.UpdateInstallations();

            }

            
            // ���������� ��������� �������
            Trolley.instance.maxHealth = _trolleyLevelsSO.GetHealth(value);
            Trolley.instance.health = _trolleyLevelsSO.GetHealth(value);



            _trolleyLevel = value;
            onUpdateValue?.Invoke();
        }
    }







    public void UpLevel(string id)
    {
        // ���� ���������� ����� - �������� ���� �� �������� � �������� �������
        if (id == "trolley" && money >= _trolleyLevelsSO.GetPrice(trolleyLevel+1))
        {
            money -= _trolleyLevelsSO.GetPrice(trolleyLevel + 1);
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
