using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerUpButton : Button
{

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (interactable) onClick.Invoke();
    }

}
