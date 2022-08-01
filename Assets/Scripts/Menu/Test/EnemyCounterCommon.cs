using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCounterCommon : MonoBehaviour
{
    public static EnemyCounterCommon instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private TextMeshProUGUI textCount;
    private int quantity;


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

}
