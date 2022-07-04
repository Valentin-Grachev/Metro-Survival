using UnityEngine;

public class ReturnableMelee_NavMeshMinion : OneMeleeTarget_NavMeshMinion
{
    [HideInInspector] public Enter_Ability ability;

    public void Return() => ability.Disable();
    
}
