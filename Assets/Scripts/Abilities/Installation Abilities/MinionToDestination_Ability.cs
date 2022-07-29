using UnityEngine;

public class MinionToDestination_Ability : ControlledAbility
{
    [SerializeField] protected GameObject _minion;


    public override void Active()
    {
        Instantiate(_minion, destination, Quaternion.identity);
    }
}
