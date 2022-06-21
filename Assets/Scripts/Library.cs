

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


    public static void LookAtTarget2D(Transform transform, Vector2 target)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
            Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg);
    }



}
