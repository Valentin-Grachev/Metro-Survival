using NTC.Global.Cache;
using UnityEngine;

public abstract class Installation : MonoCache
{
    public Animator animator { get; protected set; }


    protected virtual void Start() 
    { 
        animator = GetComponent<Animator>();
    }

    protected override void Run() { }


}
