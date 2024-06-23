using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.UI
{
public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public RectTransform background;
        public RectTransform handle;
        public Color joyStickColor;
        public Vector2 InputVector { get; private set; }

        private Image _backgroundImage;
        private Image _handleImage;
        
        private void Awake()
        {
            _backgroundImage = background.GetComponent<Image>();
            _handleImage = handle.GetComponent<Image>();
            
            _backgroundImage.color = Color.clear;
            _handleImage.color = Color.clear;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out var position))
            {
                position.x = (position.x / background.sizeDelta.x);
                position.y = (position.y / background.sizeDelta.y);

                InputVector = new Vector2(position.x * 2, position.y * 2);
                InputVector = (InputVector.magnitude > 1.0f) ? InputVector.normalized : InputVector;

                handle.anchoredPosition = new Vector2(InputVector.x * (background.sizeDelta.x / 2), InputVector.y * (background.sizeDelta.y / 2));
            }
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _backgroundImage.color = joyStickColor;
            _handleImage.color = joyStickColor;

            RectTransformUtility.ScreenPointToWorldPointInRectangle(background, eventData.position, eventData.pressEventCamera, out var worldPoint);
            background.position = worldPoint;
            
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            InputVector = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
            
            _backgroundImage.color = Color.clear;
            _handleImage.color = Color.clear;
        }
    }

}