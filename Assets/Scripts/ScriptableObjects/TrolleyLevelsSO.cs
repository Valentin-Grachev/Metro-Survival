using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "TrolleyLevels", menuName = "Gameplay/TrolleyLevels")]
public class TrolleyLevelsSO : ScriptableObject
{

    
    [HideInInspector] public float price0;
    [HideInInspector] public float price1;
    [HideInInspector] public float price2;
    [HideInInspector] public float health0;
    [HideInInspector] public float health1;
    [HideInInspector] public float health2;


    public GameObject GetPrefab(int level)
    {
        int viewNumber = GetViewNumber(level);
        return _viewChanges[viewNumber].prefab;
    }

    public int GetHealth(int level) => (int)(health0 + health1*level + health2*level*level);

    public int GetPrice(int level) => (int)(price0 + price1 * level + price2 * level * level);

    public int GetModuleQuantity(int level)
    {
        int viewNumber = GetViewNumber(level);
        Trolley trolley = _viewChanges[viewNumber].prefab.GetComponentInChildren<Trolley>();
        return trolley.moduleslotQuantity;
    }

    public int GetHeroQuantity(int level)
    {
        Trolley trolley = _viewChanges[GetViewNumber(level)].prefab.GetComponentInChildren<Trolley>();
        return trolley.heroPositions.Count;
    }

    


    public int GetViewNumber(int level)
    {
        int viewNumber = -1;
        for (int i = 0; i < _viewChanges.Count - 1; i++)
        {
            // Если следующий уровень для вида больше, значит тот номер вида, который сейчас, является нужным
            if (level < _viewChanges[i + 1].level)
            {
                viewNumber = i;
                break;
            }
        }
        // Если прошли весь цикл - значит номер вида максимальный
        if (viewNumber == -1) viewNumber = _viewChanges.Count - 1;
        return viewNumber;
    }




    [System.Serializable]
    public struct ViewChanges
    {
        public int level;
        public GameObject prefab;
    }


    [SerializeField] private List<ViewChanges> _viewChanges;



}
