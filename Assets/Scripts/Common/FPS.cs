using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{


    private TextMeshProUGUI _text;
    private int _tick;
    private float _timeUntilSecond;

    private void Start()
    {
        _tick = 0;
        _timeUntilSecond = 1f;
        _text = GetComponent<TextMeshProUGUI>();
    }


    private void Update()
    {
        _tick++;
        _timeUntilSecond -= Time.deltaTime;
        if (_timeUntilSecond <= 0f)
        {
            _text.text = _tick.ToString();
            _tick = 0;
            _timeUntilSecond = 1f;
        }
    }


}
