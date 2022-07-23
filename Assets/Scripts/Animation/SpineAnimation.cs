using UnityEngine;
using Spine.Unity;
using UnityEngine.Events;
using NTC.Global.Cache;

public enum AnimationType { Start, Idle, Move, Attack, Ability_active, Death}


public class SpineAnimation : MonoCache
{
    [Header("Animations:")]
    [SerializeField] private AnimationReferenceAsset _start;
    [SerializeField] private AnimationReferenceAsset _idle;
    [SerializeField] private AnimationReferenceAsset _move;
    [SerializeField] private AnimationReferenceAsset _attack;
    [SerializeField] private AnimationReferenceAsset _ability_active;
    [SerializeField] private AnimationReferenceAsset _death;

    [Header("Events:")]
    [SerializeField] private UnityEvent _shootEvent;
    [SerializeField] private UnityEvent _deathComplete;

    [Header("Speed:")]
    public float startSpeed = 1f;
    public float idleSpeed = 1f;
    [SerializeField] private float _moveSpeed = 1f;
    public float moveSpeed 
    { 
        get => _moveSpeed;
        set 
        { 
            _moveSpeed = value;

            // Меняем скорость анимации только если текущая - движение
            if (_currentAnimation == AnimationType.Move) skeletonAnimation.state.GetCurrent(0).TimeScale = value;
        }
    }
    [SerializeField] private float _attackSpeed = 1f;
    public float attackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;

            // Меняем скорость анимации только если текущая - атака
            if (_currentAnimation == AnimationType.Attack) skeletonAnimation.state.GetCurrent(0).TimeScale = value;
        }
    }

    public float ability_activeSpeed = 1f;
    public float deathSpeed = 1f;


    [Header("Animation State-Functions:")]
    [SerializeField] private UnityEvent _moveUpdate;
    [SerializeField] private UnityEvent _moveExit;
    [SerializeField] private UnityEvent _attackUpdate;
    [SerializeField] private UnityEvent _attackExit;





    public SkeletonAnimation skeletonAnimation { get; private set; }
    private AnimationType _currentAnimation;
    private bool workingPrivilege;


    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        workingPrivilege = false;


    }

    private void Start()
    {
        skeletonAnimation.state.Event += State_Event;
        skeletonAnimation.state.Complete += State_Complete; 
    }

    private void State_Complete(Spine.TrackEntry trackEntry)
    {
        workingPrivilege = false;
        if (_death != null && trackEntry.Animation == _death.Animation) _deathComplete.Invoke();
    }

    private void State_Event(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "shoot") _shootEvent.Invoke();
    }

    // Установка анимации, привилегированные анимации могут быть прерваны только другими привилегированными
    private void SetAnimation(AnimationReferenceAsset anim, float timeScale, bool loop, bool privilege)
    {
        if (anim == null) return;
        if (privilege) workingPrivilege = true;
        //if (workingPrivilege && !privilege) return;

        Spine.TrackEntry currentTrack = skeletonAnimation.state?.GetCurrent(0);
        // Установка новой анимации только в том случае, если она другая
        if (currentTrack == null || currentTrack.Animation != anim.Animation) 
            skeletonAnimation.state.SetAnimation(0, anim, loop).TimeScale = timeScale;

        // Но если разная скорость у одних и тех же анимаций - обновляем скорость
        else if (currentTrack.TimeScale != timeScale) currentTrack.TimeScale = timeScale;
    }

    protected override void Run()
    {
        base.Run();
        if (_currentAnimation == AnimationType.Move) _moveUpdate.Invoke();
        else if (_currentAnimation == AnimationType.Attack) _attackUpdate.Invoke();
    }




    public void SetAnimation(AnimationType animationType, bool privilege = false)
    {
        switch (animationType)
        {
            case AnimationType.Start: SetAnimation(_start, startSpeed, false, privilege); break;
            case AnimationType.Idle: SetAnimation(_idle, idleSpeed, true, privilege); break;
            case AnimationType.Move: SetAnimation(_move, moveSpeed, true, privilege); break;
            case AnimationType.Attack: SetAnimation(_attack, attackSpeed, true, privilege); break;
            case AnimationType.Ability_active: SetAnimation(_ability_active, ability_activeSpeed, false, privilege); break;
            case AnimationType.Death: SetAnimation(_death, deathSpeed, false, privilege); break;
        }
        //if (!privilege && workingPrivilege) return;
        _currentAnimation = animationType;


    }


}
