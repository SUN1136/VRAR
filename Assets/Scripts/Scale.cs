using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField] private GameObject[] axes;
    [SerializeField] private float scaleUnit = 0.25f;
    public bool showScale = false;
    public int xScale, yScale, zScale = 0;
    private int prevX, prevY, prevZ = 0;
    public Vector3 defaultScale, scale;
    private Vector3 tmpScale;
    private float xUnit, yUnit, zUnit;

    void Start()
    {
        defaultScale = transform.localScale;
        xUnit = defaultScale.x * scaleUnit;
        yUnit = defaultScale.y * scaleUnit;
        zUnit = defaultScale.z * scaleUnit;
        scale = defaultScale;
    }

    void Update()
    {
        tmpScale = defaultScale + new Vector3(xUnit*xScale, yUnit*yScale, zUnit*zScale);
        if (tmpScale.x < xUnit) {
            tmpScale.x = xUnit;
            xScale = prevX;
        }
        if (tmpScale.y < yUnit) {
            tmpScale.y = yUnit;
            yScale = prevY;
        }
        if (tmpScale.z < zUnit) {
            tmpScale.z = zUnit;
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
