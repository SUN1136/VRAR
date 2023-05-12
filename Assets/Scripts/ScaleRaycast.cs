using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRaycast : MonoBehaviour
{
    [SerializeField] private RaycastMode mode;
    [SerializeField] private InputLinker inputLinker;
    [SerializeField] private LayerMask scaleMask, objectMask;
    [SerializeField] private float distance = 10f;
    [SerializeField] private GameObject dot;
    private bool scaleUp, scaleDown = false;
    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        dot.SetActive(false);

        Ray rayObject = new Ray(transform.position, transform.forward);
        RaycastHit hitDataObject;

        if (Physics.Raycast(rayObject, out hitDataObject, distance, objectMask)) {

            if (hitDataObject.collider.gameObject.GetComponent<Scale>() != null) {
                dot.SetActive(true);
                dot.transform.localPosition = new Vector3(0f, 0f, hitDataObject.distance);
                dot.layer = 0;
                if (!mode.existColor && !mode.existObject) {
                    hitDataObject.collider.gameObject.GetComponent<Scale>().showScale = true;
                } 
            }
            else if (hitDataObject.collider.gameObject.GetComponent<ScaleAxis>() != null) {
                if (!mode.existColor && !mode.existObject) {
                    hitDataObject.collider.gameObject.GetComponent<ScaleAxis>().showScale = true;
                }
            }
            else {
                dot.SetActive(true);
                dot.transform.localPosition = new Vector3(0f, 0f, hitDataObject.distance);
                dot.layer = 0;
            }

            line.SetPosition(0, transform.position + transform.forward * 0.07f);
            line.SetPosition(1, transform.position + transform.forward * hitDataObject.distance);
        }
        else {
            line.SetPosition(0, transform.position + transform.forward * 0.07f);
            line.SetPosition(1, transform.position + transform.forward * 5f);
        }

        Ray rayScale = new Ray(transform.position, transform.forward);
        RaycastHit hitDataScale;

        if (Physics.Raycast(rayScale, out hitDataScale, distance, scaleMask)) {
            dot.SetActive(true);
            dot.transform.localPosition = new Vector3(0f, 0f, hitDataScale.distance);
            dot.layer = 8;

            line.SetPosition(0, transform.position + transform.forward * 0.07f);
            line.SetPosition(1, transform.position + transform.forward * hitDataScale.distance);

            if (inputLinker.rightJoy.y >= 0f) {
                if (inputLinker.rightJoy.y > 0.8f && !scaleUp) {
                    scaleUp = true;
                    hitDataScale.collider.gameObject.GetComponent<ScaleAxis>().scaleUp = true;
                }
                else if (inputLinker.rightJoy.y < 0.5f && scaleUp) {
                    scaleUp = false;
                }
            }
            else if (inputLinker.rightJoy.y <= 0f) {
                if (inputLinker.rightJoy.y < -0.8f && !scaleDown) {
                    scaleDown = true;
                    hitDataScale.collider.gameObject.GetComponent<ScaleAxis>().scaleDown = true;
                }
                else if (inputLinker.rightJoy.y > -0.5f && scaleDown) {
                    scaleDown = false;
                }
            }
        }
    }
}
