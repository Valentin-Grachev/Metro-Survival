using UnityEngine;

public abstract class Installation : MonoBehaviour
{
    public Animator animator { get; protected set; }


    protected virtual void Start() 
    { 
        animator = GetComponent<Animator>();
    }

    protected virtual void Update() { }
}
