using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void Awake()
    {
        var cam = GetComponent<Camera>();
        var viewportRect = cam.rect;
        const float targetAspectRatio = 9f / 16f;

        var screenAspectRatio = (float)Screen.width / Screen.height / targetAspectRatio;
            
        if (screenAspectRatio < 1)
        {
            viewportRect.height = screenAspectRatio;
            viewportRect.y = (1f - viewportRect.height) / 2f;
        }
        else
        {
            viewportRect.width = 1 / screenAspectRatio;
            viewportRect.x = (1f - viewportRect.width) / 2f;
        }
            
        cam.rect = viewportRect;
    }
}