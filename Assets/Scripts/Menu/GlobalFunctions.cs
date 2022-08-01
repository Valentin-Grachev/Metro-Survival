using UnityEngine;

public class GlobalFunctions : MonoBehaviour
{
    public static GlobalFunctions instance { get; private set; }
    private void Awake()
    {
        if (instance == null) instance = this;
    }


    public delegate void OnStartBattle();
    public event OnStartBattle onStartBattle;


    public void SetActiveTrolleyUI(bool active) => Trolley.instance.SetActiveUI(active);

    public void StartBattle() => onStartBattle?.Invoke();

}
