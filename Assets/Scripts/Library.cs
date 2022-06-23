

using UnityEngine;

public static class Library
{

    public static Transform SearchNearest(Vector3 center, float radius, LayerMask layerMask)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius, layerMask.value);

        Transform result = null;
        float minDistance = 10000f;
        foreach (var item in colliders)
        {
            if (Vector2.Distance(center, item.transform.position) < minDistance)
            {
                minDistance = Vector2.Distance(center, item.transform.position);
                result = item.transform;
            }
        }
        return result;
    }


    public static bool CompareLayer(int layerNumber, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layerNumber)) != 0;
    }


    // Расчет стартовой скорости, чтобы пуля попала в цель при заданном угле
    public static float GetStartSpeedForBallisticBullet(Vector2 startPosition, Vector2 destination, float launchAngleInDegrees, float gravityScale)
    {
        float g = Physics2D.gravity.y * gravityScale;
        float x = destination.x - startPosition.x;
        float y = destination.y - startPosition.y;
        float radAngle = launchAngleInDegrees * Mathf.PI / 180f;
        return Mathf.Sqrt(Mathf.Abs((g * x * x) / (2f * (y - Mathf.Tan(radAngle) * x) * Mathf.Pow(Mathf.Cos(radAngle), 2))));
    }






}
