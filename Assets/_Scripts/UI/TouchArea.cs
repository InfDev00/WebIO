using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.UI
{
    public class TouchArea : MonoBehaviour, IPointerDownHandler
    {
        public JoyStick joystick;

        public void OnPointerDown(PointerEventData eventData)
        {
            joystick.OnPointerDown(eventData);
        }
    }
}