using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSound : MonoBehaviour
{
    [SerializeField] private RaycastMode mode;
    [SerializeField] private InputLinker inputLinker;
    [SerializeField] private LayerMask mask;
    public AudioSource audioSource;
    public AudioClip hitSound;
    public bool makeSound = true;

    // Update is called once per frame
    void Update()
    {
        Ray rayObject = new Ray(transform.position, transform.forward);    
        RaycastHit hit;

        if (!mode.existColor && !mode.existObject)
        {
            if (Physics.Raycast(rayObject, out hit, 10f, mask))
            {
                if (makeSound == true)
                {
                    audioSource.PlayOneShot(hitSound);
                    makeSound = false;
                }
            }
            else
            {
                makeSound = true;
            }
        }
    }
}
