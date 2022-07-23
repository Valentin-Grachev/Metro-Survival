using System.Collections.Generic;
using UnityEngine;

public class AllMinions : MonoBehaviour
{
    public static AllMinions instance { get; private set; }

    private void Awake()
    {
        instance = this;
        enemies = new List<DestroyableObject>();
        allies = new List<DestroyableObject>();
    }


    public List<DestroyableObject> enemies;
    public List<DestroyableObject> allies;




}
