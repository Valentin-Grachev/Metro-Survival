using NTC.Global.Cache;
using UnityEngine;

public abstract class Installation : MonoCache
{
    public Animator animator { get; protected set; }

    protected override void OnEnabled()
    {
        base.OnEnabled();
        animator = GetComponent<Animator>();
    }


    protected virtual void Start() { }

    protected override void Run() { }


}
