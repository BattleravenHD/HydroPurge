using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Water_Gun_Controller : MonoBehaviour
{
    public List<ClenableScriptable> substanceBalls = new List<ClenableScriptable>();
    [SerializeField] Transform spawnLocation;
    [SerializeField] Transform holdLocation;
    [SerializeField] ParticleSystem waterCannon;
    [SerializeField] MeshRenderer gunWaterRenderer;
    [SerializeField] Transform washer;

    //Audios
    [SerializeField] private AudioSource gunAudioSource;
    [SerializeField] private AudioClip fireSoundEffect;
    [SerializeField] private AudioClip switchSoundEffect;

    //list of the icons
    public List<GameObject> substanceIconObjects = new List<GameObject>();

    bool fire = false;
    bool hold = false;
    int currentBallSelection = 0;
    GameObject currentlyHeldItem;
    Vector3 gunHoldingSpot;

    private void Start()
    {
        UpdateParticleSystem();
        gunHoldingSpot = washer.localPosition;

        //A
        foreach (GameObject icon in substanceIconObjects)
        {
            icon.SetActive(false);
        }

        if (substanceIconObjects.Count > 0)
        {
            substanceIconObjects[0].SetActive(true); // Enable the Water icon as the default
        }
    }

    private void LateUpdate()
    {
        if (fire && currentlyHeldItem == null)
        {
            washer.localPosition = gunHoldingSpot + new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f));
        }
        else if (!fire)
        {
            washer.localPosition = gunHoldingSpot;
        }
    }

    private void Update()
    {
        RaycastHit hit;

        if (fire && currentlyHeldItem == null)
        {
            waterCannon.Play();
        }
        else if (!fire)
        {
            waterCannon.Stop();
        }
        if (hold)
        {
            if (currentlyHeldItem == null)
            {
                if (Physics.SphereCast(Camera.main.transform.position + Camera.main.transform.forward / 2, 0.1f, Camera.main.transform.forward, out hit, 2))
                {
                    if (hit.collider.gameObject.tag == "Pickupable")
                    {
                        currentlyHeldItem = hit.collider.gameObject;
                        currentlyHeldItem.transform.position = holdLocation.position;
                        currentlyHeldItem.transform.SetParent(holdLocation);
                        currentlyHeldItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        currentlyHeldItem.GetComponent<Rigidbody>().isKinematic = true;
                        currentlyHeldItem.GetComponent<Collider>().isTrigger = true;
                    }else if (hit.collider.gameObject.tag == "Button")
                    {
                        hit.collider.GetComponent<DoorButton>().Trigger();
                    }
                }
            }else if (currentlyHeldItem.transform.parent != holdLocation)
            {
                currentlyHeldItem = null;
            }
        }
        else if (currentlyHeldItem != null)
        {
            currentlyHeldItem.transform.SetParent(null);
            currentlyHeldItem.GetComponent<Rigidbody>().isKinematic = false;
            currentlyHeldItem.GetComponent<Collider>().isTrigger = false;
            currentlyHeldItem = null;
        }
    }

    /*
    void FireCurrentlySelectedSubstance()
    {
        if (coolDown <= 0)
        {
            GameObject ball = Instantiate(substanceBalls[currentBallSelection], spawnLocation.position, Quaternion.identity);
            Rigidbody rigid = ball.GetComponent<Rigidbody>();

            rigid.velocity = spawnLocation.forward * force;
            coolDown = 0.1f;

        }
        else
        {
            coolDown -= 1 * Time.deltaTime;
        }
    }
    */
    public void OnLeftClick(InputValue value)
    {
        fire = value.Get<float>() > 0;
        gunAudioSource.clip = fireSoundEffect;

        gunAudioSource.Play();

        if (value.Get<float>() == 0) 
        {
            gunAudioSource.Stop();
        }
    }

    public void OnRightClick(InputValue value)
    {
        hold = value.Get<float>() > 0;
    }
    public void OnCycle(InputValue value)
    {
        /*
        currentBallSelection = (currentBallSelection + 1) % substanceBalls.Count;
        gunWaterRenderer.material.SetColor("_TopColour", substanceBalls[currentBallSelection].liquidColourSecondary);
        gunWaterRenderer.material.SetColor("_SIideColour", substanceBalls[currentBallSelection].liquidColourMain);
        UpdateParticleSystem();
        */

        //A
        // Disable current icon
        if (currentBallSelection < substanceIconObjects.Count)
        {
            substanceIconObjects[currentBallSelection].SetActive(false);
        }

        currentBallSelection = (currentBallSelection + 1) % substanceBalls.Count;
        gunWaterRenderer.material.SetColor("_TopColour", substanceBalls[currentBallSelection].liquidColourSecondary);
        gunWaterRenderer.material.SetColor("_SIideColour", substanceBalls[currentBallSelection].liquidColourMain);

        // Enable new icon
        if (currentBallSelection < substanceIconObjects.Count)
        {
            substanceIconObjects[currentBallSelection].SetActive(true);
        }
        gunAudioSource.PlayOneShot(switchSoundEffect);

        UpdateParticleSystem();
    }

    void UpdateParticleSystem()
    {
        ParticleSystem.MainModule settings = waterCannon.main;

        settings.startColor = new ParticleSystem.MinMaxGradient(substanceBalls[currentBallSelection].particleColor);

        settings = waterCannon.subEmitters.GetSubEmitterSystem(0).main;

        settings.startColor = new ParticleSystem.MinMaxGradient(substanceBalls[currentBallSelection].particleColor);

        settings = waterCannon.subEmitters.GetSubEmitterSystem(0).subEmitters.GetSubEmitterSystem(0).main;

        settings.startColor = new ParticleSystem.MinMaxGradient(substanceBalls[currentBallSelection].particleColor);

        waterCannon.GetComponent<ParticlesController>().collisionLayerTag = substanceBalls[currentBallSelection].LayerTag;
    }
}
