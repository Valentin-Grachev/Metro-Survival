using System.Collections.Generic;
using UnityEngine;

public class Trolley : DestroyableObject
{
    // ====== Статический сектор ======
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
    [SerializeField] protected Transform _bottomModulePosition; public Transform bottomModulePosition { get => _bottomModulePosition; }
    [SerializeField] protected Transform _middleModulePosition; public Transform middleModulePosition { get => _middleModulePosition; }
    [SerializeField] protected Transform _topModulePosition; public Transform topModulePosition { get => _topModulePosition; }
    [SerializeField] protected List<Transform> _heroPositions;
    public List<Transform> heroPositions { get => _heroPositions; }


    protected Collider2D _collider;


    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<Collider2D>();
        _trolleyCanvas.SetActive(enanleUI);
    }



    // Обновление позиций модулей и стрелков при их перемещении в разделе расстановки юнитов
    public void UpdateInstallations()
    {
        for (int i = 0; i < _heroPositions.Count; i++)
        {
            // Уничтожаем объекты всех стрелков
            if (_heroPositions[i].childCount != 0)
                Destroy(_heroPositions[i].GetComponentInChildren<Installation>().gameObject);
        }

        // Создаем объекты согласно расстановке в скрипте PlayerProgress
        for (int i = 0; i < PlayerProgress.instance.heroesId.Count; i++)
            Instantiate(IdSystem.instance.id[PlayerProgress.instance.heroesId[i]], _heroPositions[i]);



    }


    public void SetActiveUI(bool active)
    {
        _trolleyCanvas.SetActive(active);
        enanleUI = active;
    }
    



}
