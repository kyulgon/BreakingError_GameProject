using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    private Image image;

    [SerializeField] private Sprite[] sprites;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            image.sprite = sprites[index];
            index++;

            if (sprites.Length == index)
            {
                index = 0;
            }
        }
    }
}
