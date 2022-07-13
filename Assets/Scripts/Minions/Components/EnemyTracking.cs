using UnityEngine;



// Скрипт вешается на объект врага, тогда при его смерти счетчик врагов будет обновляться
public class EnemyTracking : MonoBehaviour
{
    private void Start()
    {
        DestroyableObject destrObject = GetComponent<DestroyableObject>();
        destrObject.onDeath += EnemyCounter.instance.OnDeath_Minion;
    }

}
