using UnityEngine;

public class ReturnableMelee_NavMeshMinion : OneMeleeTarget_NavMeshMinion
{
    [HideInInspector] public Enter_Ability ability;
    private bool isDisable = false;

    protected override void Start()
    {
        base.Start();
        isDisable = false;
    }


    public void Return()
    {
        if (!isDisable) ability.Disable();
        isDisable = true;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Return();
    }



}
