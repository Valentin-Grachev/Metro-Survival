using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StandartWave", menuName = "ScriptableObjects/Waves/StandartWave")]
public class StandartWave_SO : ScriptableObject
{
    [System.Serializable]
    private struct EnemyType
    {
        public string name;
        public Minion minion;
        public int quantity;

        [Tooltip("Сколько нужно сделать убийств, чтобы начал спавниться данный тип врагов.")]
        public float KillOffset;

        [Tooltip("Время между спавнами однотипных врагов.")]
        public float timeInterval;

        [Tooltip("Индивидуальное усиление - если включено, то для определения характеристик будет использоваться не общее значение усиления, а только индивидуальное.")]
        public bool useIndividualGain;

        public float individualGain;
    }



    [Tooltip("Коэффициент усиления - определяет во сколько раз сильнее будут противники относительно своих параметров, заданных в инспекторе префаба.")]
    [SerializeField] private float _gain;
    [SerializeField] private List<EnemyType> _enemyTypes;




}
