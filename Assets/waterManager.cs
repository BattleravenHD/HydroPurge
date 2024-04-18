using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class waterManager : MonoBehaviour
{
    [SerializeField] GameObject[] waterCracks;
    [SerializeField] GameObject[] moveItems;
    [SerializeField] GameObject groundWater;
    [SerializeField] AudioSource ending;
    [SerializeField] GameObject alarmLight;
    [SerializeField] AudioSource doorSoundEffect;

    [SerializeField] private Slider progressBar;

    private int totalLeaks;

    bool moving = false;

    public Camera playerCamera;
    public Camera interactionCamera;
    private bool isSwitched = false;
    private float switchBackTime;
    public float switchBackDelay = 3.0f;

    private void Start()
    {
        // Assuming waterCracks contains all the leaks/slime objects at the start.
        totalLeaks = waterCracks.Length;

        // Setup the progress bar.
        progressBar.minValue = 0;
        progressBar.maxValue = totalLeaks; // The total number of tasks.
        progressBar.value = 0; // Initially, no task is completed, so set the progress as 0.
    }

    private void Update()
    {
        bool allThere = false;
        foreach (GameObject item in waterCracks)
        {
            if (item != null)
            {
                allThere = true;
            }
        }
        if (!allThere && groundWater != null && !moving)
        {
            moving = true;
            foreach (GameObject item in moveItems)
            {
                item.transform.DOLocalMoveY(item.transform.localPosition.y + 5, 5);
            }
            transform.DOLocalMoveY(transform.localPosition.y - 1, 4);
            alarmLight.transform.DOLocalMoveY(2, 2);
            Destroy(alarmLight, 3);
            ending.Play();
            doorSoundEffect.Play();
            SwitchToInteractionCamera();
        }

        if (isSwitched && Time.time >= switchBackTime)
        {
            SwitchToPlayerCamera();
        }

        UpdateProgressBar();

    }

    void UpdateProgressBar()
    {
        // Count how many leaks have been fixed by checking how many are null (or however you denote a fixed leak).
        int fixedLeaksCount = waterCracks.Count(waterCrack => waterCrack == null);

        // Update the progress bar.
        progressBar.value = fixedLeaksCount; // This fills the bar proportionally to the count of fixed leaks.

        // Optionally, if you want to display this as text or in some other form, you can calculate it as:
        // float completionPercentage = (float)fixedLeaksCount / totalLeaks * 100;
        // And then display the 'completionPercentage' in a UI text or other component.
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
}
