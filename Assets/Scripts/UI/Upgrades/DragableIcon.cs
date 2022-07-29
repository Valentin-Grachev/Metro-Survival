using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private string _id;
    [SerializeField] private List<Image> _offRaycastImages;

    private Transform _oldParent;
    private Vector3 _oldLocalPosition;
    private Camera _camera;


    private void Start()
    {
        _camera = Camera.main;
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        _oldParent = transform.parent;
        _oldLocalPosition = transform.localPosition;
        transform.SetParent(MenuCanvas.instance.transform);
        transform.position = eventData.position;
        for (int i = 0; i < _offRaycastImages.Count; i++) _offRaycastImages[i].raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Возвращаем иконку на исходную позицию
        transform.SetParent(_oldParent);
        transform.localPosition = _oldLocalPosition;
        for (int i = 0; i < _offRaycastImages.Count; i++) _offRaycastImages[i].raycastTarget = true;


        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        // Попали в рамку
        Collider2D collider = Physics2D.OverlapCircle(mousePosition, 0.01f, 1 << LayerMask.NameToLayer("UI"));
        if (collider != null)
        {
            
            TrolleySlot trolleySlot = collider.transform.GetComponent<TrolleySlot>();

            
            // Расширяем лист до нужного размера и сохраняем id
            int number = trolleySlot.number;
            while (PlayerProgress.instance.heroesId.Count <= number) PlayerProgress.instance.heroesId.Add("Empty");
            PlayerProgress.instance.heroesId[number] = _id;


            // Если в слоте что-то было установлено раньше - удаляем
            if (collider.transform.childCount != 0) Destroy(collider.transform.GetChild(0).gameObject);

            // Создаем новый префаб
            Instantiate(IdSystem.instance.id[_id], collider.transform);
            
            




        }
    }

}
