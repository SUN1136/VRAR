using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThrough : MonoBehaviour
{
    [SerializeField] private InputLinker inputLinker;
    [SerializeField] private OVRManager ovrManager;
    [SerializeField] private MeshRenderer[] objects;
    [SerializeField] private Material skybox;
    public bool passThrough = false;
    private bool clicked = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (inputLinker.leftGrab && !clicked) {
            clicked = true;
            ovrManager.isInsightPassthroughEnabled = !ovrManager.isInsightPassthroughEnabled;
            passThrough = !passThrough;

            foreach (MeshRenderer obj in objects) {
                obj.enabled = !obj.enabled;
            }

            if (RenderSettings.skybox != null) {
                RenderSettings.skybox = null;
            }
            else {
                RenderSettings.skybox = skybox;
            }
        }
        else if (!inputLinker.leftGrab && clicked) {
            clicked = false;
        }
    }
}
