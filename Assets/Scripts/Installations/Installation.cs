using NTC.Global.Cache;
using UnityEngine;

public abstract class Installation : MonoCache
{
    public Animator animator { get; protected set; }

    [SerializeField] protected GameObject _localCanvas;

    protected override void OnEnabled()
    {
        base.OnEnabled();
        animator = GetComponent<Animator>();
        MenuFunctions.instance.onStartBattle += OnStartBattle;
    }

    private void OnStartBattle() => _localCanvas.SetActive(true);


    protected virtual void OnDestroy()
    {
        MenuFunctions.instance.onStartBattle -= OnStartBattle;
    }

    protected virtual void Start() { }

    protected override void Run() { }


}
