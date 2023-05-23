using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ColorRaycast : MonoBehaviour
{  
    public int rayLength = 10;
    public float delay = 0.1f;
    public Material lineMaterial;
    [SerializeField] private GameObject colorPalette;
    [SerializeField] private GameObject colorCanvas;
    [SerializeField] private Color colorHold = Color.clear;
    [SerializeField] private InputLinker inputLinker;
    [SerializeField] private LayerMask UIMask, FurnitureMask;
    [SerializeField] private bool showLine = false;
    [SerializeField] private RaycastMode mode;
    
    private Color[] rgbColorList = {
        new Color(224/255f, 123/255f, 57/255f, 1),
        new Color(218/255f, 87/255f, 53/255f, 1),
        new Color(214/255f, 55/255f, 51/255f, 1),
        new Color(211/255f, 55/255f, 97/255f, 1),
        new Color(208/255f, 51/255f, 138/255f, 1),

        new Color(184/255f, 51/255f, 139/255f, 1),
        new Color(149/255f, 53/255f, 137/255f, 1),
        new Color(118/255f, 54/255f, 139/255f, 1),
        new Color(69/255f, 56/255f, 140/255f, 1),
        new Color(39/255f, 79/255f, 155/255f, 1),

        new Color(56/255f, 114/255f, 178/255f, 1),
        new Color(82/255f, 163/255f, 221/255f, 1),
        new Color(86/255f, 176/255f, 129/255f, 1),
        new Color(129/255f, 185/255f, 85/255f, 1),
        new Color(150/255f, 192/255f, 84/255f, 1),

        new Color(181/255f, 205/255f, 83/255f, 1),
        new Color(248/255f, 234/255f, 84/255f, 1),
        new Color(238/255f, 205/255f, 79/255f, 1),
        new Color(229/255f, 171/255f, 73/255f, 1),
        new Color(231/255f, 148/255f, 62/255f, 1),
    };

    private Color[] greyColorList = {
        new Color(0/255f, 0/255f, 0/255f, 1),
        new Color(166/255f, 166/255f, 166/255f, 1),
        new Color(255/255f, 255/255f, 255/255f, 1),
    };


    Color angle2colorRGB(float angle){
        int index = (int)Math.Truncate((angle / 18));
        return rgbColorList[index];
    }

    Color angle2colorGrey(float angle){
        if (angle < 90){
            angle = 360 + angle;
        }
        int index = (int)Math.Truncate(((angle - 90) / 120));
        return greyColorList[index];
    }

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;

        if (!mode.existObject){
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit, rayLength * 10, UIMask)){ // Collision Exists
                if (hit.collider == colorPalette.GetComponent<Collider>() && inputLinker.rightTrigger && colorHold == Color.clear){
                    Vector3 direction_x_axis = colorCanvas.transform.right;
                    Vector3 direction_z_axis = colorCanvas.transform.forward;
                    Vector3 direction = hit.point - colorCanvas.transform.position;

                    float theta = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(direction_x_axis, direction) / (direction.magnitude * direction_x_axis.magnitude));
                    bool isPositiveAngle = Vector3.Dot(Vector3.Cross(direction_x_axis, direction), direction_z_axis) > 0;

                    theta = isPositiveAngle ? theta : 360 - theta;

                    Debug.Log(direction.magnitude);
                    
                    if (direction.magnitude < 0.475 * 0.333 * 0.51282051282){
                        colorHold = angle2colorGrey(theta);
                    }
                    else if (direction.magnitude >= 0.475 * 0.333 * 0.51282051282 && direction.magnitude < 0.475 * 0.333)
                    {
                        colorHold = angle2colorRGB(theta);
                    }
                }
                
                Vector3 v1 = transform.position;
                v1 = transform.TransformPoint(Vector3.forward * rayLength);
                
                if (showLine) {
                    GameObject myLine = new GameObject();
                    myLine.transform.position = transform.position;
                    myLine.AddComponent<LineRenderer>();

                    LineRenderer lr = myLine.GetComponent<LineRenderer>();
                    lr.material = lineMaterial;
                    lr.startWidth = 0.01f;
                    lr.endWidth = 0.01f;
                    lr.SetPosition(0, transform.position);
                    lr.SetPosition(1, hit.point);
                    GameObject.Destroy(myLine, delay);
                }
            }
            else if (Physics.Raycast(ray, out hit, rayLength * 10, FurnitureMask)){
                if (!inputLinker.rightTrigger && colorHold != Color.clear){
                    Renderer renderer = hit.collider.gameObject.GetComponent<MeshRenderer>();
                    renderer.material = new Material(shader: Shader.Find("Diffuse"));
                    renderer.material.color = colorHold;
                    colorHold = Color.clear;
                }

                Vector3 v1 = transform.position;
                v1 = transform.TransformPoint(Vector3.forward * rayLength);

                if (showLine) {
                    GameObject myLine = new GameObject();
                    myLine.transform.position = transform.position;
                    myLine.AddComponent<LineRenderer>();

                    LineRenderer lr = myLine.GetComponent<LineRenderer>();
                    lr.material = lineMaterial;
                    lr.startWidth = 0.01f;
                    lr.endWidth = 0.01f;
                    lr.SetPosition(0, transform.position);
                    lr.SetPosition(1, hit.point);
                    GameObject.Destroy(myLine, delay);
                }
            }
            else { // No Collision

                Vector3 v1 = transform.position;
                v1 = transform.TransformPoint(Vector3.forward * rayLength);

                if (showLine) {
                    GameObject myLine = new GameObject();
                    myLine.transform.position = transform.position;
                    myLine.AddComponent<LineRenderer>();

                    LineRenderer lr = myLine.GetComponent<LineRenderer>();
                    lr.material = lineMaterial;
                    lr.startWidth = 0.01f;
                    lr.endWidth = 0.01f;
                    lr.SetPosition(0, transform.position);
                    lr.SetPosition(1, v1);
                    GameObject.Destroy(myLine, delay);
                }
            }

            if (!inputLinker.rightTrigger){
                colorHold = Color.clear;
            }
        }

        if (colorHold == Color.clear) {
            mode.existColor = false;
        }
        else {
            mode.existColor = true;
        }
    }
}
