using NTC.Global.Cache;
using UnityEngine;

public abstract class Installation : MonoCache
{
    [SerializeField] protected GameObject _localCanvas;

    public SpineAnimation spineAnimation { get; protected set; }


    protected virtual void Awake()
    {
        spineAnimation = GetComponent<SpineAnimation>();
    }

    protected virtual void Start()
    {
        MenuFunctions.instance.onStartBattle += OnStartBattle;
    }

    private void OnStartBattle()
    {
        if (_localCanvas != null) _localCanvas.SetActive(true);
    }
    


    protected virtual void OnDestroy()
    {
        MenuFunctions.instance.onStartBattle -= OnStartBattle;
    }



}
