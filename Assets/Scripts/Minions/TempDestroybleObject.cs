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
        if (timeUntilDestroy < 0) Death();
    }




}
