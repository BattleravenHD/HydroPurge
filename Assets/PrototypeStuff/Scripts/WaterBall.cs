using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    [SerializeField] LayerMask slimeLayer;
    [SerializeField] GameObject splash;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Slimes>(out Slimes slime))
        {
            ContactPoint[] contacts = new ContactPoint[0];
            contacts = collision.contacts;

            foreach (ContactPoint point in contacts)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, (point.point - transform.position), out hit, 1, slimeLayer))
                {
                    slime.ShrinkTriangle(hit.triangleIndex);
                }
            }
        }

        Transform splashs = Instantiate(splash, collision.GetContact(0).point, Quaternion.identity).transform;
        splashs.LookAt(transform);
        Destroy(gameObject);
    }
}
