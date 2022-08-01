using System.Collections.Generic;
using UnityEngine;


public enum Overlap { Circle, Box}

public static class Library
{

    public static Transform SearchNearestCircle(Vector2 center, float radius, LayerMask layerMask)
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

    public static Transform SearchNearestBox(Vector2 center, Vector2 size, LayerMask layerMask)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, 0f, layerMask.value);

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

    public static bool SearchTransformInScanningBox(Transform search, Vector2 center, Vector2 size, LayerMask layerMask)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, 0f, layerMask.value);
        foreach (var item in colliders)
            if (search == item.transform) return true;

        return false;
    }



    public static bool CompareLayer(int layerNumber, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layerNumber)) != 0;
    }


    public static float GetStartSpeedForBallisticBullet(Vector2 startPosition, Vector2 destination, float launchAngleInDegrees, float gravityScale)
    {
        float g = Physics2D.gravity.y * gravityScale;
        float x = destination.x - startPosition.x;
        float y = destination.y - startPosition.y;
        float radAngle = launchAngleInDegrees * Mathf.PI / 180f;
        return Mathf.Sqrt(Mathf.Abs((g * x * x) / (2f * (y - Mathf.Tan(radAngle) * x) * Mathf.Pow(Mathf.Cos(radAngle), 2))));
    }


    public static Vector2 GetTargetPositionWithPrediction(Vector2 targetPosition, Vector2 targetVelocity, float distance)
    {
        float gainPrediction = distance / Constants.distancePrediction;
        return targetPosition + targetVelocity * gainPrediction;
    }

    public static bool IsCollided(Vector2 position, Team teamForCollide, out DestroyableObject collidedObject)
    {
        // ¬ зависимости от команды выбираем в каком списке будем искать
        List<DestroyableObject> list = null;
        if (teamForCollide == Team.Enemy) list = AllMinions.instance.enemies;
        else if (teamForCollide == Team.Ally) list = AllMinions.instance.allies;

        for (int i = 0; i < list.Count; i++)
        {
            if (ObjectIsInsideArea(position, (Vector2)list[i].transform.position + list[i].bulletCollider.offset, list[i].bulletCollider.size))
            {
                collidedObject = list[i];
                return true;
            }
        }

        collidedObject = null;
        return false;

    }

    public static bool ObjectIsInsideArea(Vector2 position, Vector2 areaCenter, Vector2 areaSize)
    {
        return areaCenter.x - areaSize.x/2f < position.x && position.x < areaCenter.x + areaSize.x / 2f
            && areaCenter.y - areaSize.y / 2f < position.y && position.y < areaCenter.y + areaSize.y / 2f;
    }


    public static bool TryFindNearestUntilLine(Vector2 center, float linePositionX, Team detectionableTeam, out DestroyableObject foundObject)
    {
        // ¬ зависимости от команды выбираем в каком списке будем искать
        List<DestroyableObject> list = null;
        if (detectionableTeam == Team.Enemy) list = AllMinions.instance.enemies;
        else if (detectionableTeam == Team.Ally) list = AllMinions.instance.allies;

        float minDistance = 10000f;
        foundObject = null;

        // ѕроход по всем созданным объектам и выбор того, кто находитс€ внутри области
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].transform.position.x < linePositionX
                && Vector2.Distance(center, list[i].transform.position) < minDistance)
            {
                foundObject = list[i];
                minDistance = Vector2.Distance(center, list[i].transform.position);
            }
        }

        if (foundObject != null) return true;
        else return false;





    }

    public static bool TryFindNearestInsideArea(Vector2 center, Vector2 area, Team detectionableTeam, out DestroyableObject foundObject)
    {
        // ¬ зависимости от команды выбираем в каком списке будем искать
        List<DestroyableObject> list = null;
        if (detectionableTeam == Team.Enemy) list = AllMinions.instance.enemies;
        else if (detectionableTeam == Team.Ally) list = AllMinions.instance.allies;

        float minDistance = 10000f;
        foundObject = null;

        // ѕроход по всем созданным объектам и выбор того, кто находитс€ внутри области
        for (int i = 0; i < list.Count; i++)
        {
            if (area.x/2 > Mathf.Abs(list[i].transform.position.x - center.x) 
                && area.y / 2 > Mathf.Abs(list[i].transform.position.y - center.y) 
                && Vector2.Distance(center, list[i].transform.position) < minDistance)
            {
                foundObject = list[i];
                minDistance = Vector2.Distance(center, list[i].transform.position);
            }   
        }

        if (foundObject != null) return true;
        else return false;
        
    }

    public static bool TryFindInsideAreaAll(Vector2 center, Vector2 area, Team detectionableTeam, out List<DestroyableObject> foundObjects)
    {
        foundObjects = new List<DestroyableObject>();

        // ¬ зависимости от команды выбираем в каком списке будем искать
        List<DestroyableObject> list;
        if (detectionableTeam == Team.Enemy) list = AllMinions.instance.enemies;
        else list = AllMinions.instance.allies;

        // ѕроход по всем созданным объектам и выбор того, кто находитс€ внутри области
        for (int i = 0; i < list.Count; i++)
        {
            if (area.x > Mathf.Abs(list[i].transform.position.x - center.x)
                && area.y > Mathf.Abs(list[i].transform.position.x - center.x))
            {
                foundObjects.Add(list[i]);
            }
        }

        // Ќичего не нашли
        if (foundObjects.Count == 0) return false;
        else return true;
        
    }



}
