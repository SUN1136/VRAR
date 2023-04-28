using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAxis : MonoBehaviour
{
    [SerializeField] private Scale furniture;
    [SerializeField] [Range(0, 2)] private int axis;
    [SerializeField] private float length = 0.2f;
    public bool scaleUp, scaleDown, showScale = false;
    private Vector3 defaultScale = new Vector3(0.1f, 0.2f, 0.1f);
    private Vector3 defaultRot;

    void Start()
    {
        if (axis == 0) defaultRot = new Vector3(0f, 0f, -90f);
        else if (axis == 1) defaultRot = new Vector3(0f, 0f, 0f);
        else if (axis == 2) defaultRot = new Vector3(90f, 0f, 0f);

        defaultScale.y = length;
    }

    void Update()
    {
        if (showScale) {
            furniture.showScale = true;
            showScale = false;
        }

        if (scaleUp) {
            if (axis == 0) furniture.xScale += 1;
            else if (axis == 1) furniture.yScale += 1;
            else if (axis == 2) furniture.zScale += 1;
            scaleUp = false;
        }

        if (scaleDown) {
            if (axis == 0) furniture.xScale -= 1;
            else if (axis == 1) furniture.yScale -= 1;
            else if (axis == 2) furniture.zScale -= 1;
            scaleDown = false;
        }

        if (axis == 0) transform.localScale = new Vector3(defaultScale.x / furniture.scale.y, defaultScale.y / furniture.scale.x, defaultScale.z / furniture.scale.z);
        else if (axis == 1) transform.localScale = new Vector3(defaultScale.x / furniture.scale.x, defaultScale.y / furniture.scale.y, defaultScale.z / furniture.scale.z);
        else if (axis == 2) transform.localScale = new Vector3(defaultScale.x / furniture.scale.x, defaultScale.y / furniture.scale.z, defaultScale.z / furniture.scale.y);

        if (axis == 0) transform.localPosition = new Vector3(defaultScale.y / furniture.scale.x, 0f, 0f);
        else if (axis == 1) transform.localPosition = new Vector3(0f, defaultScale.y / furniture.scale.y, 0f);
        else if (axis == 2) transform.localPosition = new Vector3(0f, 0f, defaultScale.y / furniture.scale.z);
    }
}
