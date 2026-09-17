using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    public GameObject Joystick;
    public GameObject JoystickBG;

    public Vector2 JoystickVec;

    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    private RectTransform joystickRect;
    private RectTransform joystickBGRect;

    void Start()
    {
        JoystickVec = Vector2.zero;
        Joystick.SetActive(false);
        JoystickBG.SetActive(false);
        joystickRect = Joystick.GetComponent<RectTransform>();
        joystickBGRect = JoystickBG.GetComponent<RectTransform>();

        joystickOriginalPos = joystickRect.anchoredPosition;
        joystickRadius = joystickBGRect.rect.width / 4f;
    }

    public void PointerDown(BaseEventData baseEventData)
    {
        Joystick.SetActive(true);
        JoystickBG.SetActive(true);
        PointerEventData pointerEventData = baseEventData as PointerEventData;

        joystickTouchPos = pointerEventData.position;

        // Gebruik screen position voor de eerste plaatsing
        joystickRect.position = joystickTouchPos;
        joystickBGRect.position = joystickTouchPos;

        JoystickVec = Vector2.zero;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;

        Vector2 dragPos = pointerEventData.position;

        // Richting vanaf het punt waar de speler indrukte
        Vector2 direction = dragPos - joystickTouchPos;

        float distance = direction.magnitude;

        if (distance > 0)
        {
            JoystickVec = direction.normalized;
        }
        else
        {
            JoystickVec = Vector2.zero;
        }

        // Stick binnen de joystick houden
        float clampedDistance = Mathf.Min(distance, joystickRadius);

        joystickRect.position =
            joystickTouchPos + JoystickVec * clampedDistance;
    }

    public void PointerUp(BaseEventData baseEventData)
    {
        Joystick.SetActive(false);
        JoystickBG.SetActive(false);
        JoystickVec = Vector2.zero;

        joystickRect.anchoredPosition = joystickOriginalPos;
        joystickBGRect.anchoredPosition = joystickOriginalPos;
    }
}