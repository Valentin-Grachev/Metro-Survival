using UnityEngine;

public abstract class Installation : MonoBehaviour
{
    protected Animator _animator;


    protected virtual void Start() 
    { 
        _animator = GetComponent<Animator>();
    }

    protected virtual void Update() { }
}
