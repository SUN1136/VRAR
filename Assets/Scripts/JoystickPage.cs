using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPage : MonoBehaviour
{
    [SerializeField] private InputLinker inputLinker;
    [SerializeField] private int pageNum;
    [SerializeField] private GameObject[] canvases;
    public int page;

    public float radius, angle, offset;
    private float unitAngle;

    void Start()
    {
        unitAngle = 360f / pageNum;
        if (pageNum % 2 != 0) {
            offset = 90f;
        }
        else {
            offset = 90f - unitAngle/2f;
        }
    }

    void Update()
    {
        radius = inputLinker.leftJoy.magnitude;
        angle = Mathf.Atan2(inputLinker.leftJoy.y, inputLinker.leftJoy.x) * Mathf.Rad2Deg - offset;
        if (angle < 0f) {
            angle += 360f;
        }
        if (angle == 360f) {
            angle = 0f;
        }

        if (radius > 0.3f) {
            page = (int)Mathf.Floor(angle / unitAngle);
        }

        for (int i = 0; i < pageNum; i++) {
            if (i == page) {
                canvases[i].SetActive(true);
            }
            else {
                canvases[i].SetActive(false);
            }
        }
    }
}
