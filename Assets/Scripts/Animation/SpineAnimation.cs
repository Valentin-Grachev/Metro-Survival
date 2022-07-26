using UnityEngine;
using Spine.Unity;
using UnityEngine.Events;
using NTC.Global.Cache;

public enum AnimationType { Start, Idle, Move, Attack, Ability_active, Death, Ability_deactive, None}


public class SpineAnimation : MonoCache
{
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
    public float ability_deactiveSpeed = 1f;
    public float deathSpeed = 1f;





    [Header("Events:")]
    [SerializeField] private UnityEvent _shootEvent;
    [SerializeField] private UnityEvent _abilityEvent;
    [SerializeField] private UnityEvent _deathComplete;
    [SerializeField] private UnityEvent _abilityComplete;

    


    [Header("Animation State-Functions:")]
    [SerializeField] private UnityEvent _moveUpdate;
    [SerializeField] private UnityEvent _moveExit;
    [SerializeField] private UnityEvent _attackUpdate;
    [SerializeField] private UnityEvent _attackExit;





    public SkeletonAnimation skeletonAnimation { get; private set; }
    private AnimationType _currentAnimation; public AnimationType currentAnimation { get => _currentAnimation; }
    private AnimationType _privilegeAnimation;


    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        _privilegeAnimation = AnimationType.None;


    }

    private void Start()
    {
        skeletonAnimation.state.Event += State_Event;
        skeletonAnimation.state.Complete += State_Complete;
        skeletonAnimation.state.End += State_End;
    }

    private void State_End(Spine.TrackEntry trackEntry)
    {
        
        if (_move != null && trackEntry.Animation == _move.Animation) _moveExit.Invoke();
        else if (_attack != null && trackEntry.Animation == _attack.Animation) _attackExit.Invoke();

        
    }

    private void State_Complete(Spine.TrackEntry trackEntry)
    {

        // События
        if (_currentAnimation == AnimationType.Death) _deathComplete.Invoke();
        else if (trackEntry.Animation == _ability_active.Animation) _abilityComplete.Invoke();


        // Сброс привилегий
        if (_currentAnimation == _privilegeAnimation) _privilegeAnimation = AnimationType.None;
    }

    private void State_Event(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "shoot") _shootEvent.Invoke();
        else if (e.Data.Name == "ability") _abilityEvent.Invoke();
    }

    // Установка анимации, привилегированные анимации могут быть прерваны только другими привилегированными
    private void SetAnimation(AnimationReferenceAsset anim, float timeScale, bool loop)
    {
        if (anim == null) return;
        if (TryGetComponent(out Installation inst)) print(gameObject.name + ": " + anim.ToString());
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
        if (animationType == AnimationType.None || _privilegeAnimation != AnimationType.None
            || (animationType == _currentAnimation && !privilege)) return;

        if (privilege) _privilegeAnimation = animationType;

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
        if (animationType != AnimationType.None) _currentAnimation = animationType;


    }


}
