using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    [SerializeField] GameObject splash;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            Destroy(collision.gameObject);
        }

        Transform splashs = Instantiate(splash, collision.GetContact(0).point, Quaternion.identity).transform;
        splashs.LookAt(transform);
        Destroy(gameObject);
    }
}
