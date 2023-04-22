using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLinker : MonoBehaviour
{
    [SerializeField] private bool debug;
    public bool leftTrigger, rightTrigger = false;
    public float leftTriggerValue, rightTriggerValue = 0f;
    public bool leftGrab, rightGrab = false;
    public float leftGrabValue, rightGrabValue = 0f;
    public bool leftX, leftY, rightA, rightB = false;
    public Vector2 leftJoy, rightJoy = new Vector2(0f, 0f);
    
    void Start()
    {
        
    }

    void Update()
    {
        if (!debug) {
            leftTriggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            if (leftTriggerValue > 0.5f) leftTrigger = true;
            else leftTrigger = false;
            
            rightTriggerValue = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
            if (rightTriggerValue > 0.5f) rightTrigger = true;
            else rightTrigger = false;
            
            leftGrabValue = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
            if (leftGrabValue > 0.5f) leftGrab = true;
            else leftGrab = false;

            rightGrabValue = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
            if (rightGrabValue > 0.5f) rightGrab = true;
            else rightGrab = false;

            leftX = OVRInput.GetDown(OVRInput.Button.Three);
            leftY = OVRInput.GetDown(OVRInput.Button.Four);
            rightA = OVRInput.GetDown(OVRInput.Button.One);
            rightB = OVRInput.GetDown(OVRInput.Button.Two);

            leftJoy = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            rightJoy = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        }
    }
}
