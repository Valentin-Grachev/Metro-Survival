using System.Collections.Generic;
using UnityEngine;

public class Trolley : DestroyableObject
{
    // ====== ����������� ������ ======
    public static Trolley instance { get; private set; }
    protected override void Awake()
    {
        instance = this;
        base.Awake();
    }
    private static bool enanleUI = false;




    [Header("Trolley:")]
    [SerializeField] protected GameObject _trolleyCanvas;
    [SerializeField] protected Transform _spawnPosition; public Transform spawnPosition { get => _spawnPosition; }
    [SerializeField] protected int _moduleSlotQuantity; public int moduleslotQuantity { get => _moduleSlotQuantity; }
    [SerializeField] protected List<Transform> _heroPositions;
    public List<Transform> heroPositions { get => _heroPositions; }



    protected override void Start()
    {
        base.Start();
        _trolleyCanvas.SetActive(enanleUI);
    }



    // ���������� ������� ������� � �������� ��� �� ����������� � ������� ����������� ������
    public void UpdateInstallations()
    {
        for (int i = 0; i < _heroPositions.Count; i++)
        {
            // ���������� ������� ���� ��������
            if (_heroPositions[i].childCount != 0)
                Destroy(_heroPositions[i].GetComponentInChildren<Installation>().gameObject);
        }

        // ������� ������� �������� ����������� � ������� PlayerProgress
        for (int i = 0; i < PlayerProgress.instance.heroesId.Count; i++)
            Instantiate(IdSystem.instance.id[PlayerProgress.instance.heroesId[i]], _heroPositions[i]);



    }


    public void SetActiveUI(bool active)
    {
        _trolleyCanvas.SetActive(active);
        enanleUI = active;
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        Destroy(transform.parent.gameObject);
    }



}
