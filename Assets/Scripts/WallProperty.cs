using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallProperty : MonoBehaviour
{
    public PassThrough passThrough;
    public Vector3 vertex1, vertex2, vertex3, vertex4;
    void Start()
    {

    }

    void Update()
    {
        if (passThrough.passThrough)
        {
            Color color = GetComponent<MeshRenderer>().material.color;
            color.a = 0.25f;
            GetComponent<MeshRenderer>().material.color = color;
        }
        else
        {
            Color color = GetComponent<MeshRenderer>().material.color;
            color.a = 0.4f;
            GetComponent<MeshRenderer>().material.color = color;
        }
    }
}
