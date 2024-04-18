using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] GameObject[] doors;
    [SerializeField] Light doorLight;
    [SerializeField] bool activePainter;
    [SerializeField] ParticleSystem painter;
    [SerializeField] private AudioSource gunAudioSource;
    [SerializeField] private AudioClip doorSoundEffect;

    private bool doorActivated = false;

    public Camera playerCamera;
    public Camera interactionCamera;
    private bool isSwitched = false;
    private float switchBackTime;
    public float switchBackDelay = 5.0f;

    public void Trigger()
    {
        //triggered = true;

        if (doorActivated) // Check if the door is already activated
            return; // If yes, exit the method without doing anything

        doorActivated = true; // Set the flag indicating that the door is now activated

        doorLight.color = Color.green;
        if (activePainter)
        {
            painter.Play();
        }
        else
        {
            foreach (GameObject item in doors)
            {
                item.transform.DOLocalMoveY(3, 2);
            }
        }

        gunAudioSource.PlayOneShot(doorSoundEffect);
        SwitchToInteractionCamera();
    }

    void SwitchToInteractionCamera()
    {
        playerCamera.enabled = false;
        interactionCamera.enabled = true;
        isSwitched = true;
        switchBackTime = Time.time + switchBackDelay;
    }
    void SwitchToPlayerCamera()
    {
        playerCamera.enabled = true;
        interactionCamera.enabled = false;
        isSwitched = false;
    }
    void Update()
    {
        if (isSwitched && Time.time >= switchBackTime)
        {
            SwitchToPlayerCamera();
        }
    }

}
