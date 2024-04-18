using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField] GameObject youWin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            youWin.SetActive(true);
        }
    }
}
