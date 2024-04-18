using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    public List<GameObject> waterBalls = new List<GameObject>();
    [SerializeField] Transform spawnLocation;
    [SerializeField] Transform holdLocation;
    [SerializeField] float force = 10;

    bool fire = false;
    bool hold = false;
    float cooldown = 0;
    int currentBallSelection = 0;
    GameObject currentlyHeldItem;

    private void Update()
    {
        if (fire && currentlyHeldItem == null)
        {
            //DrawOnTexture();
            ShrinkModel();
        }
        if (hold)
        {
            if (currentlyHeldItem == null)
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward / 2, Camera.main.transform.forward, out hit, 1))
                {
                    if (hit.collider.gameObject.tag == "Pickupable")
                    {
                        currentlyHeldItem = hit.collider.gameObject;
                        currentlyHeldItem.transform.position = holdLocation.position;
                        currentlyHeldItem.transform.SetParent(holdLocation);
                        currentlyHeldItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        currentlyHeldItem.GetComponent<Rigidbody>().isKinematic = true;
                        currentlyHeldItem.GetComponent<Collider>().isTrigger = true;
                    }
                }
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

    void ShrinkModel()
    {
        if (cooldown <= 0)
        {
            GameObject ball = Instantiate(waterBalls[currentBallSelection], spawnLocation.position, Quaternion.identity);
            Rigidbody rigid = ball.GetComponent<Rigidbody>();

            rigid.velocity = spawnLocation.forward * force;
            cooldown = 0.1f;

        }else
        {
            cooldown -= 1 * Time.deltaTime;
        }
    }

    void DrawOnTexture()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        Debug.DrawRay(Camera.main.transform.position, ray.direction);
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Slime")
            {
                Renderer rend = hit.transform.GetComponent<Renderer>();

                if (rend == null)
                    return;

                Texture2D oldtex = rend.material.GetTexture("_Mask") as Texture2D;

                Texture2D newTex = new Texture2D(oldtex.width, oldtex.height);

                newTex.SetPixels(oldtex.GetPixels());

                Vector2 pixelUV = hit.textureCoord;
                pixelUV.x *= newTex.width;
                pixelUV.y *= newTex.height;

                newTex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
                newTex.Apply();

                rend.material.SetTexture("_Mask", newTex);

                Debug.Log(hit.textureCoord);
            }
        }
    }

    public void OnLeftClick(InputValue value)
    {
        fire = value.Get<float>() > 0;
    }

    public void OnRightClick(InputValue value)
    {
        hold = value.Get<float>() > 0;
    }
    public void OnCycle(InputValue value)
    {
        currentBallSelection = (currentBallSelection + 1) % waterBalls.Count;
    }
}
