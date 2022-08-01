using FunkyCode;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public delegate void OnChangeSettings();
    public event OnChangeSettings onChangeLight;

    public static GameSettings instance { get; private set; }

    private void Awake() => instance = this;



    [Header("Components:")]
    [SerializeField] private LightingManager2D _lightingManager2D;

    [Header("UI:")]
    [SerializeField] private Toggle _lightToggle;
    [SerializeField] private Toggle _cameraShakeToggle;





    private void Start() => Load();



    private void Load()
    {
        lightEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("Lighting", 1));
        cameraShakeEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("CameraShake", 1));
        
    }


    
    public bool lightEnabled {
        get => _lightingManager2D.gameObject.activeInHierarchy;
        set
        {
            _lightingManager2D.gameObject.SetActive(value);
            PlayerPrefs.SetInt("Lighting", Convert.ToInt32(value));
            _lightToggle.isOn = value;
            onChangeLight?.Invoke();
        }
    }
    


    private bool _cameraShake;
    public bool cameraShakeEnabled
    {
        get => _cameraShake;
        set
        {
            _cameraShake = value;
            PlayerPrefs.SetInt("CameraShake", Convert.ToInt32(value));
            _cameraShakeToggle.isOn = value;
        }
    }





}
