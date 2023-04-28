using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField] private GameObject[] axes;
    [SerializeField] private float scaleUnit = 0.5f;
    public bool showScale = false;
    public int xScale, yScale, zScale = 0;
    private int prevX, prevY, prevZ = 0;
    public Vector3 defaultScale, scale;
    private Vector3 tmpScale;

    void Start()
    {
        defaultScale = transform.localScale;
        scale = defaultScale;
    }

    void Update()
    {
        tmpScale = defaultScale + new Vector3(scaleUnit*xScale, scaleUnit*yScale, scaleUnit*zScale);
        if (tmpScale.x < scaleUnit) {
            tmpScale.x = scaleUnit;
            xScale = prevX;
        }
        if (tmpScale.y < scaleUnit) {
            tmpScale.y = scaleUnit;
            yScale = prevY;
        }
        if (tmpScale.z < scaleUnit) {
            tmpScale.z = scaleUnit;
            zScale = prevZ;
        }
        scale = tmpScale;
        transform.localScale = scale;

        prevX = xScale;
        prevY = yScale;
        prevZ = zScale;

        if (!showScale) {
            foreach (GameObject axis in axes) {
                axis.SetActive(false);
            }
        }
        else if (showScale) {
            foreach (GameObject axis in axes) {
                axis.SetActive(true);
            }
        }

        showScale = false;
    }
}
