using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRaycast : MonoBehaviour
{
    [SerializeField] private InputLinker inputLinker;
    [SerializeField] private RaycastMode mode;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float distance = 10f;
    [SerializeField] private GameObject guideCanvas;
    [SerializeField] private GameObject guideButton;
    [SerializeField] private bool guideOn = true;
    private bool clicked = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!mode.existColor && !mode.existObject) {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitData;

            if (Physics.Raycast(ray, out hitData, distance, mask)) {
                if (hitData.collider.gameObject == guideButton) {
                    if (inputLinker.rightTrigger && !clicked) {
                        guideOn = !guideOn;
                        clicked = true;
                    }
                    else if (!inputLinker.rightTrigger && clicked) {
                        clicked = false;
                    }
                }
            }
        }

        if (guideOn) {
            guideCanvas.SetActive(true);
        }
        else {
            guideCanvas.SetActive(false);
        }
    }
}
