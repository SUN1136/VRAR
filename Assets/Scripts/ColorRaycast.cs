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
    [SerializeField] private bool existColor = false;
    [SerializeField] private Color colorHold = Color.clear;
    [SerializeField] private InputLinker inputLinker;
    [SerializeField] private LayerMask UIMask, FurnitureMask;
    
    private Color[] colorList = {
        new Color(234/255f, 50/255f, 35/255f, 1),
        new Color(240/255f, 145/255f, 53/255f, 1),
        new Color(246/255f, 192/255f, 66/255f, 1),
        new Color(251/255f, 228/255f, 77/255f, 1),
        new Color(51/255f, 253/255f, 84/255f, 1),
        new Color(225/255f, 252/255f, 82/255f, 1),
        new Color(194/255f, 251/255f, 80/255f, 1),
        new Color(154/255f, 250/255f, 78/255f, 1),
        new Color(117/255f, 250/255f, 108/255f, 1),
        new Color(116/255f, 250/255f, 167/255f, 1),
        new Color(116/255f, 250/255f, 208/255f, 1),
        new Color(115/255f, 251/255f, 239/255f, 1),
    };

    Color angle2color(float angle){
        int index = (int)Math.Truncate((angle / 30));
        return colorList[index];
    }

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        GameObject colorPalette = GameObject.Find("ColorPalette");
        GameObject colorCanvas = GameObject.Find("ColorCanvas");

        if (true){
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit, rayLength * 10, UIMask)){ // Collision Exists
                if (hit.collider == colorPalette.GetComponent<Collider>() && inputLinker.rightTrigger && colorHold == Color.clear){
                    Vector3 direction_x_axis = colorCanvas.transform.right;
                    Vector3 direction_z_axis = colorCanvas.transform.forward;
                    Vector3 direction = hit.point - colorCanvas.transform.position;
    
                    float theta = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(direction_x_axis, direction) / (direction.magnitude * direction_x_axis.magnitude));
                    bool isPositiveAngle = Vector3.Dot(Vector3.Cross(direction_x_axis, direction), direction_z_axis) > 0;

                    theta = isPositiveAngle ? theta : 360 - theta;
                    colorHold = angle2color(theta);
                }
                
                Vector3 v1 = transform.position;
                v1 = transform.TransformPoint(Vector3.forward * rayLength);

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
            else if (Physics.Raycast(ray, out hit, rayLength * 10, FurnitureMask)){
                if (!inputLinker.rightTrigger && colorHold != Color.clear){
                    Renderer renderer = hit.collider.gameObject.GetComponent<MeshRenderer>();
                    renderer.material = new Material(shader: Shader.Find("Diffuse"));
                    renderer.material.color = colorHold;
                    colorHold = Color.clear;
                }

                Vector3 v1 = transform.position;
                v1 = transform.TransformPoint(Vector3.forward * rayLength);

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
            else { // No Collision
                if (!inputLinker.rightTrigger){
                    colorHold = Color.clear;
                }

                Vector3 v1 = transform.position;
                v1 = transform.TransformPoint(Vector3.forward * rayLength);

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
    }
}
