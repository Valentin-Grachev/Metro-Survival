using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    public static MenuCanvas instance { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

}
