using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;
    public float handleRange = 1f;

    private Vector2 inputVector = Vector2.zero;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - (Vector2)joystickBackground.position;
        inputVector = (direction.magnitude > joystickBackground.sizeDelta.x / 2f) 
            ? direction.normalized 
            : direction / (joystickBackground.sizeDelta.x / 2f);
        joystickHandle.anchoredPosition = inputVector * joystickBackground.sizeDelta.x / 2f * handleRange;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public Vector2 GetInput()
    {
        return inputVector;
    }
}