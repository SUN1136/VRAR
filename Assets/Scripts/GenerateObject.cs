using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateObject : MonoBehaviour
{
    [SerializeField] private RaycastMode mode;
    [SerializeField] private InputLinker inputLinker;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float unit = 0.25f;
    [SerializeField] private float rotUnit = 15f;
    [SerializeField] private Transform furnitureParent;
    public GameObject[] objectPrefab;
    public GameObject   objectToDrag;
    public bool         isTriggered = false;
    public bool         Grabbed = false;
    public bool         xPrevious = false;
    public bool         yPrevious = false;
    public float        moveSpeed = 0.01f;
    public float        rotSpeed = 10000f;
    private float       rotAngle = 0f;
    private float       distance = 2f;

    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    public void Drag()
    {
        
    }

    public void Update()
    {
        if (!mode.existColor) {
            Ray rayObject = new Ray(transform.position, transform.forward);
            // Raycast from the controller to get the intersection point
            RaycastHit hit;


            if (Physics.Raycast(rayObject, out hit, 10f, mask))
            {           
                Debug.Log("I hitted the ::::::::::::::::::: " + hit.collider.gameObject.name);
                if (!isTriggered && inputLinker.rightTrigger && Grabbed == false)
                {
                    GameObject obj = null;
                    if (hit.collider.gameObject.GetComponent<ButtonIndex>())
                    {
                        int idx = hit.collider.gameObject.GetComponent<ButtonIndex>().furnitureIdx;
                        distance = 1.4f;
                        obj = Instantiate(objectPrefab[idx], transform.position + transform.forward * distance, Quaternion.identity, furnitureParent);
                        obj.layer = LayerMask.NameToLayer("Furniture");
 
                        Grabbed = true;
                        rotAngle += 180;
                        objectToDrag = obj;
                    }
                    else if (hit.collider.gameObject.layer == 6)
                    {
                        objectToDrag = hit.collider.gameObject.transform.parent.parent.gameObject;
                        rotAngle = objectToDrag.transform.eulerAngles.y;
                        distance = (objectToDrag.transform.position - transform.position).magnitude;
                        Grabbed = true;
                    }

                }
            }
            
            // If we left the rightTrigger, Then all the function works again
            if (!inputLinker.rightTrigger && Grabbed == true)
            {
                objectToDrag = null;
                Grabbed = false;
                rotAngle = 0;
            }
            if (objectToDrag)
            {
                if (inputLinker.rightJoy.y > 0.7 || inputLinker.rightJoy.y < -0.7)
                {
                    if (yPrevious == false)
                    {
                        if (inputLinker.rightJoy.y > 0.7) {
                            distance += unit;
                        }
                        else if (inputLinker.rightJoy.y < -0.7) {
                            distance -= unit;
                        }
                        yPrevious = true;
                    }
                }
                else {
                    yPrevious = false;
                }

                if (inputLinker.rightJoy.y > 0.7 || inputLinker.rightJoy.y < -0.7) {
                    distance += inputLinker.rightJoy.y * moveSpeed; // add movement in the y-axis based on joystick input
                }
                distance = Mathf.Clamp(distance, 0, 10);
                Vector3 newPosition = transform.position + transform.forward * distance;
                objectToDrag.transform.position = new Vector3(Mathf.Round(newPosition.x / unit) * unit , Mathf.Clamp(Mathf.Round(newPosition.y / unit) * unit, 0, 10), Mathf.Round(newPosition.z / unit) * unit);
                
                if (inputLinker.rightJoy.x > 0.7 || inputLinker.rightJoy.x < -0.7)
                {
                    if (xPrevious == false)
                    {
                        if (inputLinker.rightJoy.x > 0.7) {
                            rotAngle += rotUnit;
                        }
                        else if (inputLinker.rightJoy.x < -0.7) {
                            rotAngle -= rotUnit;
                        }
                        xPrevious = true;
                    }
                }
                else {
                    xPrevious = false;
                }
                if (inputLinker.rightJoy.x > 0.7 || inputLinker.rightJoy.x < -0.7)
                {
                    rotAngle += inputLinker.rightJoy.x * rotSpeed * Time.deltaTime;
                }
                float tmpAngle = Mathf.Round(rotAngle / rotUnit);
                Quaternion newRotation = Quaternion.Euler(0f, tmpAngle * rotUnit, 0f);
                objectToDrag.transform.rotation = newRotation;
            }
        }

        if (objectToDrag) {
            mode.existObject = true;
        }
        else {
            mode.existObject = false;
        }

        isTriggered = inputLinker.rightTrigger;
    }
}
