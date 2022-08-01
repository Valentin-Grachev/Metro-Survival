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

        [Tooltip("������� ����� ������� �������, ����� ����� ���������� ������ ��� ������.")]
        public float KillOffset;

        [Tooltip("����� ����� �������� ���������� ������.")]
        public float timeInterval;

        [Tooltip("�������������� �������� - ���� ��������, �� ��� ����������� ������������� ����� �������������� �� ����� �������� ��������, � ������ ��������������.")]
        public bool useIndividualGain;

        public float individualGain;
    }



    [Tooltip("����������� �������� - ���������� �� ������� ��� ������� ����� ���������� ������������ ����� ����������, �������� � ���������� �������.")]
    [SerializeField] private float _gain;
    [SerializeField] private List<EnemyType> _enemyTypes;




}
