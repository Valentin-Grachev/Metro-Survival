using UnityEngine;



// ������ �������� �� ������ �����, ����� ��� ��� ������ ������� ������ ����� �����������
public class EnemyTracking : MonoBehaviour
{
    private void Start()
    {
        DestroyableObject destrObject = GetComponent<DestroyableObject>();
        destrObject.onDeath += EnemyCounter.instance.OnDeath_Minion;
    }

}
