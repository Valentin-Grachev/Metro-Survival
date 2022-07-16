using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TrolleyLevels", menuName = "Gameplay/TrolleyLevels")]
public class TrolleyLevelsSO : ScriptableObject
{
    [System.Serializable]
    public struct TrolleyLevel
    {
        public int price;
        public int health;
        public int heroSlots;
        public int moduleSlots;
        public GameObject trolleyPrefab;
    }

    [SerializeField] private List<TrolleyLevel> _levels; public List<TrolleyLevel> levels { get => _levels; }



}
