using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPaletteControl : MonoBehaviour
{
    // public Image image;
    public bool isActive = false;

    void Start()
    {
        
    }

    void Update()
    {
        Image image = GetComponent<Image>();

        if (isActive && !image.enabled){
            image.enabled = true;
        }
        if (!isActive && image.enabled){
            image.enabled = false;
        }
    }
}
