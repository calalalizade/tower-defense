using UnityEngine;
using UnityEngine.EventSystems;

public class Build : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [HideInInspector]
    public static GameObject ActiveCanvas;
    private GameObject canvas;
    private bool pressed = false;

    void Start()
    {
        canvas = transform.Find("Canvas").gameObject;
        canvas.SetActive(false);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (pressed) return;
        if (ActiveCanvas != null) ActiveCanvas.SetActive(false);
        pressed = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!pressed) return;
        pressed = false;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (!pressed) return;
        pressed = false;
        canvas.SetActive(true);
        ActiveCanvas = canvas;
    }
}
