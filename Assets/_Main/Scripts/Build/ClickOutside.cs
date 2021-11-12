using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOutside : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Build.ActiveCanvas != null)
        {
            Build.ActiveCanvas.SetActive(false);
        }
    }
}
