using NTC.Global.Cache;
using TMPro;
using UnityEngine;

public class EnemyCounter : MonoCache
{

    [SerializeField] private TextMeshProUGUI textCount;

    private int _quatity;
    public int quantity 
    { 
        get => _quatity;
        private set
        {
            _quatity = value;
            textCount.text = value.ToString();
        }
    }


    private void Start() => quantity = 0;


    protected override void Run()
    {
        base.Run();
        quantity = AllMinions.instance.enemies.Count;
    }



}
