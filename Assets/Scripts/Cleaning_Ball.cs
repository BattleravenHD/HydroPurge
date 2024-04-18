using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Cleaning_Ball : MonoBehaviour
{
    [SerializeField] LayerMask collisionLayer;
    [SerializeField] GameObject particleObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Cleanable_Base>(out Cleanable_Base interacted))
        {
            RaycastHit hit;
            foreach (ContactPoint point in collision.contacts)
            {
                if (Physics.Raycast(transform.position, point.point - transform.position, out hit, 1, collisionLayer))
                {
                    interacted.Interact(point.point, transform);
                }
            }
        }

        Transform splashs = Instantiate(particleObject, collision.GetContact(0).point, Quaternion.identity).transform;
        splashs.LookAt(transform);

        Destroy(gameObject);
    }
}
