using UnityEngine;

public class TempDestroybleObject : DestroyableObject
{
    public delegate void UpdateTimeUntilDestroy(float time, float maxTime);
    public event UpdateTimeUntilDestroy onUpdateTimeUntilDestroy;

    [HideInInspector] public float timeUntilDestroy;
    [HideInInspector] public float maxTimeUntilDestroy;

    protected override void Run()
    {
        base.Run();
        timeUntilDestroy -= Time.deltaTime;
        onUpdateTimeUntilDestroy.Invoke(timeUntilDestroy, maxTimeUntilDestroy);
        if (timeUntilDestroy < 0 && !isDeath) Death();
    }


    protected override void Death()
    {
        base.Death();
        GetComponent<Collider2D>().enabled = false;
        NavMeshRebaker.instance.Rebake();
    }




}
