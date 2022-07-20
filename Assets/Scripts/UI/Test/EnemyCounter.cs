using TMPro;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public static EnemyCounter instance { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    [SerializeField] private TextMeshProUGUI textCount;
    public int quantity;


    private void Start()
    { 
        quantity = 0;
        textCount.text = quantity.ToString();
    }


    public void OnCreate_Minion()
    {
        quantity++;
        textCount.text = quantity.ToString();
    }

    public void OnDeath_Minion()
    {
        quantity--;
        textCount.text = quantity.ToString();
    }

}
