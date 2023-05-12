using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorUI : MonoBehaviour
{
    [SerializeField] private InputLinker inputLinker;
    [SerializeField] private GameObject leftHandAnchor;
    public GameObject panel;
    private bool panelActive = false;
    private bool buttonClicked = false;
    private Vector3 offset;

    private void Start()
    {
        Vector3 bottomCenter = transform.TransformPoint(new Vector3(0, -transform.localScale.y, 0));
        offset = bottomCenter - transform.position;
    }
    void Update()
    {
        // Set the position of the object to be the same as the position of the LeftHandAnchor game object
        panel.transform.position = leftHandAnchor.transform.position - offset;

        // Set the rotation of the object to be the same as the rotation of the LeftHandAnchor game object
        panel.transform.rotation = leftHandAnchor.transform.rotation;

        if (inputLinker.leftX && !buttonClicked)
        {
            if (!panelActive)
            {
                panel.SetActive(true);
                panelActive = true;
            }
            else
            {
                panel.SetActive(false);
                panelActive = false;
            }
            buttonClicked = true;
        }
        if (buttonClicked && !inputLinker.leftX)
            buttonClicked = false;
    }
}
