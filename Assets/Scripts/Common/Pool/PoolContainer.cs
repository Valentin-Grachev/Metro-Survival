using UnityEngine;

public class PoolContainer : MonoBehaviour
{
    public static PoolContainer container { get; private set; }


    private void Awake()
    {
        container = this;
    }

}
