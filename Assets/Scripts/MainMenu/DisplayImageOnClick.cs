using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayImageOnClick : MonoBehaviour, IPointerDownHandler
{
    public Image image;

    void Start()
    {
        // Turns the image off.
        image.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Turns the image on if it is off, and off if it is on.
        Debug.Log("Pressed");
        image.enabled = !image.enabled;
    }
}