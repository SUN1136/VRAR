using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorUI : MonoBehaviour
{
    [SerializeField] private InputLinker inputLinker;
    [SerializeField] private GameObject leftHandAnchor;
    public GameObject canvas;
    public GameObject panel;
    public GameObject colorPalette;
    // private bool panelActive = false;
    private bool canvasButtonClicked = false;
    private bool buttonClicked = false;
    private Vector3 offset;
    private bool uiOn = false;

    private void Start()
    {
        Vector3 bottomCenter = transform.TransformPoint(new Vector3(0, -transform.localScale.y, 0));
        offset = bottomCenter - transform.position;
    }
    void Update()
    {
        if (inputLinker.leftTrigger && !canvasButtonClicked){
            canvasButtonClicked = true;
            if (canvas.activeSelf){
                canvas.SetActive(false);
                uiOn = false;
            }
            else {
                canvas.SetActive(true);
                uiOn = true;
            }
        }
        else if (canvasButtonClicked && !inputLinker.leftTrigger) {
            canvasButtonClicked = false;
        }
    }
}
