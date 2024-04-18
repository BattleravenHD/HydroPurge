using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cement : Cleanable_Base
{
    [SerializeField] float increaseAmount = 1f;
    [SerializeField] GameObject[] deleteItems;

    Material mat;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        CompletionPercentage = 0;
    }

    public override void Interact(Vector3 collisionPoint, Transform interactionObject)
    {
        CompletionPercentage += increaseAmount;

        mat.SetFloat("_Fill", CompletionPercentage / 100);
        
        if (CompletionPercentage == CompletionMin)
        {
            foreach (GameObject item in deleteItems)
            {
                Destroy(item);
            }
            deleteItems = new GameObject[] { };
        }
    }
}
