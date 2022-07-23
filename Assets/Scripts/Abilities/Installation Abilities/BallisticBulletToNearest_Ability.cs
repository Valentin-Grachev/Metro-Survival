using UnityEngine;

public class BallisticBulletToNearest_Ability : BallisticBulletToDestination_Ability
{
    protected Transform _nearestTarget;
    protected Vector2 _lastNearestPosition;

    public override void Active()
    {
        
        if (_nearestTarget != null)
        {
            // ����� � ����������� - �������������� ����� ����� ����������
            Vector2 velocity = _nearestTarget.GetComponent<Rigidbody2D>().velocity;
            Vector2 nearestPosition = new Vector2(_nearestTarget.position.x, AbilityDestination.instance.aimPosition.y);

            // �������� ���������� - �� ������� �������� ���, ����� ��� ������ � ������ �������
            destination = Library.GetTargetPositionWithPrediction(nearestPosition, velocity, Vector2.Distance(transform.position, nearestPosition))
                + new Vector2(_damageArea.x * 0.7f, 0f);
        }
        else
        {
            destination = Library.GetTargetPositionWithPrediction(
                _lastNearestPosition, Vector2.zero, Vector2.Distance(transform.position, _lastNearestPosition));
        }
        
        
        base.Active();
    }


    public override void Enable()
    {
        //_nearestTarget = Library.SearchNearestCircle(transform.position, _installation.detectionRadius, _installation.enemyLayer);
        if (_nearestTarget == null) return;

        _lastNearestPosition = _nearestTarget.position;

        // ������������� ��������������� ����� ����������, ��� ��������� ���� ��������
        destination = _nearestTarget.transform.position;

        base.Enable();
    }



}
