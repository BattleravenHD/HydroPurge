using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityUI : MonoBehaviour
{
    public GameObject uiElement; // Reference to the TextMeshPro element you want to show/hide
    public Transform cameraTransform; // Reference to the player's camera transform

    private void Update()
    {
        // If the UI is active, make it face the camera
        if (uiElement.activeSelf)
        {
            FaceCamera();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideUI();
        }
    }

    private void ShowUI()
    {
        uiElement.SetActive(true);
    }

    private void HideUI()
    {
        uiElement.SetActive(false);
    }

    private void FaceCamera()
    {
        // This will make the UI's forward direction point towards the camera
        // It assumes that the camera is looking in its forward direction.
        uiElement.transform.forward = cameraTransform.forward;
    }
}
