using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageFilter : MonoBehaviour
{
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        image.fillAmount = Mathf.Lerp(0, 1, timer);
    }
}
