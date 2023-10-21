using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBehavior : MonoBehaviour
{

    public float fadeSpeed = 0.05f;
    private Image image;
    public bool fadeFromStart = true;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeFromStart)
        {
            float alpha = Mathf.Lerp(0f, 1f, Time.deltaTime * fadeSpeed);
            Color newColor = image.color;
            newColor.a += alpha;
            image.color = newColor;
            if(image.color.a >= 1f)
            {
                fadeFromStart = false;
            }
        }
        else
        {
            float alpha = Mathf.Lerp(0f, 1f, Time.deltaTime * fadeSpeed);
            Color newColor = image.color;
            newColor.a -= alpha;
            image.color = newColor;
        }
    }
}
