using UnityEngine;

public class TempDestroybleObject : DestroyableObject
{
    public delegate void UpdateTimeUntilDestroy(float time, float maxTime);
    public event UpdateTimeUntilDestroy onUpdateTimeUntilDestroy;

    public float timeUntilDestroy;
    public float maxTimeUntilDestroy;

    protected override void Update()
    {
        base.Update();
        timeUntilDestroy -= Time.deltaTime;
        onUpdateTimeUntilDestroy.Invoke(timeUntilDestroy, maxTimeUntilDestroy);
        if (timeUntilDestroy < 0) Destroy(gameObject);
    }




}
