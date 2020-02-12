using UnityEngine;

//Borrowed from http://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html
public class CameraController : MonoBehaviour
{
    public float horizontalRatio = 16.0f;
    public float verticalRatio = 9.0f;
    void Start()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = horizontalRatio / verticalRatio;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();
        if(camera == null)
        { 
            return;
        }

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) * 0.5f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) * 0.5f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
