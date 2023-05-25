using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallAnchor : MonoBehaviour
{
    [SerializeField] private RectTransform wallAnchor;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] public float unit = 2.5f;
    public PassThrough passThrough;
    private Image floorAnchor;
    private InputLinker inputLinker;
    private Transform rightHand;
    private GameObject wall, wallParent;
    private bool AClicked = true;
    private bool ActiveClicked = false;
    private float bound = 250f;
    private float scale = 5f;
    public int rotState = 1;
    public Vector3 vertex1, vertex2, vertex3, vertex4;
    void Start()
    {
        floorAnchor = transform.parent.gameObject.GetComponent<Image>();
        inputLinker = transform.parent.gameObject.GetComponent<FloorAnchor>().inputLinker;
        rightHand = transform.parent.gameObject.GetComponent<FloorAnchor>().rightHand;
        wallParent = transform.parent.gameObject.GetComponent<FloorAnchor>().wallParent;

        transform.localPosition = new Vector3(0f, 0f, 0f);

        if (rotState == 1) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    void Update()
    {
        if (passThrough.passThrough) {
            Color color = wallAnchor.gameObject.GetComponent<Image>().color;
            color.a = 0.25f;
            wallAnchor.gameObject.GetComponent<Image>().color = color;
        }
        else {
            Color color = wallAnchor.gameObject.GetComponent<Image>().color;
            color.a = 1f;
            wallAnchor.gameObject.GetComponent<Image>().color = color;
        }

        if (inputLinker.rightGrab && !ActiveClicked) {
            ActiveClicked = true;

            if (rotState == 0) {
                rotState = 1;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else {
                rotState = 0;
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }
        else if (!inputLinker.rightGrab && ActiveClicked) {
           ActiveClicked = false;
        }

        float k = 0f;
        float x = 0f;
        float y = 0f;
        Vector3 pos = rightHand.position;
        Vector3 front = rightHand.forward;
        if (rotState == 0) {
            if (front.x != 0) {
                k = (transform.position.x - pos.x) / front.x;
                if (k >= 0f) {
                    x = Mathf.Clamp(-(pos.z + k*front.z - transform.position.z)*scale, -bound, bound);
                    y = Mathf.Clamp((pos.y + k*front.y - transform.position.y)*scale, 0f, bound);

                    x = unit * Mathf.Round(x / unit);
                    y = unit * Mathf.Round(y / unit);
                }
            }

            if (k >= 0f) {
                if (x >= 0f) {
                    wallAnchor.offsetMin = new Vector2(bound, bound);
                    wallAnchor.offsetMax = new Vector2(-bound + x, -bound + y);
                }
                else {
                    wallAnchor.offsetMin = new Vector2(bound + x, bound);
                    wallAnchor.offsetMax = new Vector2(-bound, -bound + y);
                }
            }
        }
        else {
            if (front.z != 0) {
                k = (transform.position.z - pos.z) / front.z;
                if (k >= 0f) {
                    x = Mathf.Clamp(-(pos.x + k*front.x - transform.position.x)*scale, -bound, bound);
                    y = Mathf.Clamp((pos.y + k*front.y - transform.position.y)*scale, 0f, bound);

                    x = unit * Mathf.Round(x / unit);
                    y = unit * Mathf.Round(y / unit);
                }
            }

            if (k >= 0f) {
                if (x >= 0f) {
                    wallAnchor.offsetMin = new Vector2(bound, bound);
                    wallAnchor.offsetMax = new Vector2(-bound + x, -bound + y);
                }
                else {
                    wallAnchor.offsetMin = new Vector2(bound + x, bound);
                    wallAnchor.offsetMax = new Vector2(-bound, -bound + y);
                }
            }
        }

        if (inputLinker.rightA && !AClicked) {
            AClicked = true;

            wall = Instantiate(wallPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, wallParent.transform);

            if (rotState == 0) {
                wall.transform.position = transform.position + new Vector3(0f, y/(2f*scale), -x/(2f*scale));
                wall.transform.localScale = new Vector3(Mathf.Abs(x/(10f*scale)), 1f, y/(10f*scale));
                wall.transform.rotation = Quaternion.Euler(90, 90, 0);

                vertex1 = transform.position;
                vertex2 = transform.position + new Vector3(0f, y/scale, -x/scale);
                vertex3 = transform.position + new Vector3(0f, y/scale, 0f);
                vertex4 = transform.position + new Vector3(0f, 0f, -x/scale);
            }
            else {
                wall.transform.position = transform.position + new Vector3(-x/(2f*scale), y/(2f*scale), 0f);
                wall.transform.localScale = new Vector3(Mathf.Abs(x/(10f*scale)), 1f, y/(10f*scale));
                wall.transform.rotation = Quaternion.Euler(90, 180, 0);

                vertex1 = transform.position;
                vertex2 = transform.position + new Vector3(-x/(2f*scale), y/(2f*scale), 0f);
                vertex3 = transform.position + new Vector3(-x/scale, 0f, 0f);
                vertex4 = transform.position + new Vector3(0f, y/scale, 0f);
            }

            wall.GetComponent<WallProperty>().vertex1 = vertex1;
            wall.GetComponent<WallProperty>().vertex2 = vertex2;
            wall.GetComponent<WallProperty>().vertex3 = vertex3;
            wall.GetComponent<WallProperty>().vertex4 = vertex4;

            wall.GetComponent<WallProperty>().passThrough = passThrough;

            WallProperty wallChild = wall.transform.GetChild(0).gameObject.GetComponent<WallProperty>();
            wallChild.vertex1 = vertex1;
            wallChild.vertex2 = vertex2;
            wallChild.vertex3 = vertex3;
            wallChild.vertex4 = vertex4;
            wallChild.passThrough = passThrough;

            transform.parent.gameObject.GetComponent<FloorAnchor>().finishAnchor = true;
            Destroy(gameObject);
        }
        else if (!inputLinker.rightA && AClicked) {
            AClicked = false;
        }

        if (!floorAnchor.enabled) {
            Destroy(gameObject);
        }
    }
}
