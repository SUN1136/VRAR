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
    public GameObject[] objectPrefab;
    public GameObject objectToDrag;
    public bool Grabbed = false;
    public float moveSpeed = 0.01f;
    public float rotSpeed = 10000f;
    private float rotAngle = 0f;
    private float distance = 2f;

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
                if (inputLinker.rightTrigger && Grabbed == false)
                {
                    GameObject obj = null;
                    if (hit.collider.gameObject.GetComponent<ButtonIndex>())
                    {
                        int idx = hit.collider.gameObject.GetComponent<ButtonIndex>().furnitureIdx;
                        distance = 2f;
                        obj = Instantiate(objectPrefab[idx], transform.position + transform.forward * distance, Quaternion.identity);
                        Grabbed = true;
                        rotAngle += 180;
                        objectToDrag = obj;
                    }
                    else if (hit.collider.gameObject.layer == 6)
                    {
                        objectToDrag = hit.collider.gameObject;
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
                distance += inputLinker.rightJoy.y * moveSpeed; // add movement in the y-axis based on joystick input
                distance = Mathf.Clamp(distance, 0, 10);
                Vector3 newPosition = transform.position + transform.forward * distance;
                rotAngle += inputLinker.rightJoy.x * rotSpeed * Time.deltaTime;
                float tmpAngle = Mathf.Round(rotAngle / rotUnit);
                Debug.Log(rotAngle);
                Quaternion newRotation = Quaternion.Euler(0f, tmpAngle * rotUnit, 0f);
                // Set the position and rotation of the object to match the controller's position and rotation
                objectToDrag.transform.position = new Vector3(Mathf.Round(newPosition.x / unit) * unit , Mathf.Clamp(Mathf.Round(newPosition.y / unit) * unit, 0, 10), Mathf.Round(newPosition.z / unit) * unit);
                objectToDrag.transform.rotation = newRotation;
            }
        }

        if (objectToDrag) {
            mode.existObject = true;
        }
        else {
            mode.existObject = false;
        }
    }
}
