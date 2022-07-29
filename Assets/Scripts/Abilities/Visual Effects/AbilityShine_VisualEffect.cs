using UnityEngine;

public class AbilityShine_VisualEffect : MonoBehaviour
{
    private SpineAnimation spineAnimation;


    private void Start()
    {
        spineAnimation = GetComponent<SpineAnimation>();
        var ability = GetComponentInParent<ShooterInstallationAbility>();
        ability.onEnable += OnEnable_Ability;
        ability.onDisable += OnDisable_Ability;
    }

    private void OnDisable_Ability() => spineAnimation.SetAnimation(AnimationType.Death);

    private void OnEnable_Ability() => spineAnimation.SetAnimation(AnimationType.Ability_active);


    public void SetIdleAnimation() => spineAnimation.SetAnimation(AnimationType.Idle);



}
