using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disolveable : Cleanable_Base
{
    [SerializeField] float reductionAmount = 0.1f;

    Material mat;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        CompletionPercentage = 0;
    }

    public override void Interact(Vector3 collisionPoint, Transform interactionObject)
    {
        CompletionPercentage += reductionAmount;

        mat.SetFloat("_Disolve", CompletionPercentage / 100);

        if (CompletionPercentage > CompletionMin)
        {
            Destroy(gameObject);
        }
    }
}
