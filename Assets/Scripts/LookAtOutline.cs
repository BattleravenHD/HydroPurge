using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtOutline : MonoBehaviour
{
    public Camera cam;
    private int defaultLayer;
    private int outlinedLayer;
    private GameObject lastHitObject;

    public LayerMask interactableMask;  // The mask of objects that can be outlined
    public string outlineLayerName = "Outlined";  // The name of the layer you created earlier
    [SerializeField] float raycastDistance = 2.0f;

    private void Start()
    {
        cam = Camera.main;
        defaultLayer = LayerMask.NameToLayer("No Outline");
        outlinedLayer = LayerMask.NameToLayer(outlineLayerName);
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, interactableMask))
        {
            if (hit.transform.gameObject.layer == defaultLayer)
            {
                if (lastHitObject != hit.transform.gameObject)
                {
                    // Reset the previous object if it's not the one being looked at now
                    if (lastHitObject)
                    {
                        lastHitObject.layer = defaultLayer;
                    }

                    hit.transform.gameObject.layer = outlinedLayer;
                    lastHitObject = hit.transform.gameObject;
                }
            }
        }
        else
        {
            // If we're not looking at an interactable object, reset the last object's layer
            if (lastHitObject)
            {
                lastHitObject.layer = defaultLayer;
                lastHitObject = null;
            }
        }
    }
}
