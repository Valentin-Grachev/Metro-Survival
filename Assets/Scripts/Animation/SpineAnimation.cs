using UnityEngine;
using Spine.Unity;
using UnityEngine.Events;
using NTC.Global.Cache;

public enum AnimationType { Start, Idle, Move, Attack, Ability_active, Death, Ability_deactive, None}


public class SpineAnimation : MonoCache
{
    [Header("Develop:")] [SerializeField] bool testing = false;

    [Header("Animations:")]
    [SerializeField] private AnimationReferenceAsset _start;
    [SerializeField] private AnimationReferenceAsset _idle;
    [SerializeField] private AnimationReferenceAsset _move;
    [SerializeField] private AnimationReferenceAsset _attack;
    [SerializeField] private AnimationReferenceAsset _ability_active;
    [SerializeField] private AnimationReferenceAsset _ability_deactive;
    [SerializeField] private AnimationReferenceAsset _death;

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
            if (skeletonAnimation.state != null &&
                skeletonAnimation.state.GetCurrent(0).Animation == _move.Animation)
                skeletonAnimation.state.GetCurrent(0).TimeScale = value;
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
            if (skeletonAnimation.state.GetCurrent(0) != null &&
                skeletonAnimation.state.GetCurrent(0).Animation == _attack.Animation)
                skeletonAnimation.state.GetCurrent(0).TimeScale = value;
        }
    }

    public float ability_activeSpeed = 1f;
    public float ability_deactiveSpeed = 1f;
    public float deathSpeed = 1f;





    [Header("Events:")]
    [SerializeField] private UnityEvent _shootEvent;
    [SerializeField] private UnityEvent _abilityEvent;
    [SerializeField] private UnityEvent _deathComplete;
    [SerializeField] private UnityEvent _abilityComplete;
    [SerializeField] private UnityEvent _startAbility;

    


    [Header("Animation State-Functions:")]
    [SerializeField] private UnityEvent _moveUpdate;
    [SerializeField] private UnityEvent _moveExit;
    [SerializeField] private UnityEvent _attackUpdate;
    [SerializeField] private UnityEvent _attackExit;

    public SkeletonAnimation skeletonAnimation { get; private set; }

    private bool _canSetLoopAnimation;
    private bool _interruptedStartAbility;


    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        _canSetLoopAnimation = true;
        _interruptedStartAbility = false;
    }

    private void Start()
    {
        skeletonAnimation.state.Event += State_Event;
        skeletonAnimation.state.Complete += State_Complete;
        skeletonAnimation.state.End += State_End;
        skeletonAnimation.state.Interrupt += State_Interrupt;
    }

    private void State_Interrupt(Spine.TrackEntry trackEntry)
    {
        if (_ability_active != null && trackEntry.Animation == _ability_active.Animation)
        {
            if (!_interruptedStartAbility) _startAbility.Invoke();
            _interruptedStartAbility = !_interruptedStartAbility;

        }
    }


    protected override void OnEnabled()
    {
        base.OnEnabled();
    }


    private void State_End(Spine.TrackEntry trackEntry)
    {
        if (_move != null && trackEntry.Animation == _move.Animation) _moveExit.Invoke();
        else if (_attack != null && trackEntry.Animation == _attack.Animation) _attackExit.Invoke();
    }

    private void State_Complete(Spine.TrackEntry trackEntry)
    {
        // Нециклическая анимация закончилась - можно ставить циклические
        if (!trackEntry.Loop) _canSetLoopAnimation = true;

        
        if (_death != null && trackEntry.Animation == _death.Animation) _deathComplete.Invoke();
        else if (_ability_active != null && trackEntry.Animation == _ability_active.Animation) _abilityComplete.Invoke();

    }

    private void State_Event(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "shoot") _shootEvent.Invoke();
        else if (e.Data.Name == "ability") _abilityEvent.Invoke();
    }

    private void SetAnimation(AnimationReferenceAsset anim, float timeScale, bool loop)
    {
        Spine.TrackEntry currentTrack = skeletonAnimation.state.GetCurrent(0);
        // Это нециклическая анимация - устанавливаем прерывая любую другую
        if (!loop)
        {
            skeletonAnimation.state.SetAnimation(0, anim, loop).TimeScale = timeScale;
            _canSetLoopAnimation = false;

        }

        // Установка новой анимации только в том случае, если она другая
        // Циклические анимации не могут прервать нециклические
        else if (currentTrack.Animation != anim.Animation && _canSetLoopAnimation)
        {
            skeletonAnimation.state.SetAnimation(0, anim, loop).TimeScale = timeScale;
        }
            

    }

    protected override void Run()
    {
        Spine.TrackEntry currentTrack = skeletonAnimation.state.GetCurrent(0);
        if (currentTrack == null) return;

        base.Run();
        if (_move != null && currentTrack.Animation == _move.Animation) _moveUpdate.Invoke();
        else if (_attack != null && currentTrack.Animation == _attack.Animation) _attackUpdate.Invoke();
    }




    public void SetAnimation(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.Start: SetAnimation(_start, startSpeed, false); break;
            case AnimationType.Idle: SetAnimation(_idle, idleSpeed, true); break;
            case AnimationType.Move: SetAnimation(_move, moveSpeed, true); break;
            case AnimationType.Attack: SetAnimation(_attack, attackSpeed, true); break;
            case AnimationType.Ability_active: SetAnimation(_ability_active, ability_activeSpeed, false); break;
            case AnimationType.Death: SetAnimation(_death, deathSpeed, false); break;
            case AnimationType.Ability_deactive: SetAnimation(_ability_deactive, ability_deactiveSpeed, false); break;
            case AnimationType.None: break;
        }


    }


}
