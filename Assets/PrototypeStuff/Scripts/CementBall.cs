using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CementBall : MonoBehaviour
{
    [SerializeField] GameObject cementSwatch;
    [SerializeField] GameObject splash;

    void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Cementable>(out Cementable cement))
        {
            Transform item = Instantiate(cementSwatch, collision.GetContact(0).point, Quaternion.identity).transform;

            item.SetParent(collision.transform);
        }
        Transform splashs = Instantiate(splash, collision.GetContact(0).point, Quaternion.identity).transform;
        splashs.LookAt(transform);
        Destroy(gameObject);
    }
}
