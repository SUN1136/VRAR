using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRaycast : MonoBehaviour
{
    [SerializeField] private InputLinker inputLinker;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float distance = 10f;
    private bool destroyed = false;
    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, distance, mask)) {
            if (inputLinker.rightThumb && !destroyed) {
                destroyed = true;
                Destroy(hitData.collider.gameObject);
            }
            else if (!inputLinker.rightThumb && destroyed) {
                destroyed = false;
            }
        }
        else {
            if (inputLinker.rightThumb && !destroyed) {
                destroyed = true;
            }
            else if (!inputLinker.rightThumb && destroyed) {
                destroyed = false;
            }
        }
    }
}
