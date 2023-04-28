using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorAnchor : MonoBehaviour
{
    [SerializeField] private PassThrough passThrough;
    [SerializeField] public InputLinker inputLinker;
    [SerializeField] public Transform rightHand;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] public GameObject wallParent;
    [SerializeField] public float unit = 0.5f;
    private Image image;
    public bool anchorOn, wallAnchorOn, finishAnchor = false;
    private bool wallTrigger = true;
    private float bound = 10f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (inputLinker.rightA && !anchorOn && !finishAnchor) {
            anchorOn = true;
            wallTrigger = true;
        }
        
        if ((inputLinker.rightB || finishAnchor) && anchorOn) {
            anchorOn = false;
            wallAnchorOn = false;
        }

        if (!inputLinker.rightA && finishAnchor) {
            finishAnchor = false;
        }

        if (anchorOn && !wallAnchorOn) {
            Vector3 front = rightHand.forward;
            Vector3 pos = rightHand.position;

            if (front.y < 0) {
                image.enabled = true;

                float k = (-0.01f - pos.y) / front.y;
                float x = Mathf.Clamp((pos.x + k*front.x), -bound, bound);
                float z = Mathf.Clamp((pos.z + k*front.z), -bound, bound);

                x = unit * Mathf.Round(x / unit);
                z = unit * Mathf.Round(z / unit);

                transform.position = new Vector3(x, 0.01f, z);
            }
            else {
                image.enabled = false;
            }

            if (inputLinker.rightA && !wallTrigger) {
                wallTrigger = true;
                wallAnchorOn = true;
            }
            else if (!inputLinker.rightA && wallTrigger) {
                wallTrigger = false;
            }
        }
        else if (anchorOn && wallAnchorOn) {
            if (transform.childCount == 0) {
                GameObject wallCanvas = Instantiate(wallPrefab, transform.position, Quaternion.Euler(0, 90, 0), transform);
                wallCanvas.GetComponent<WallAnchor>().passThrough = passThrough;
            }
        }
        else if (!anchorOn) {
            image.enabled = false;
            wallAnchorOn = false;
        }
    }
}
