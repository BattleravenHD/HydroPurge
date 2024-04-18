using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Cleanable_Base : MonoBehaviour
{
    public string LayerTag;
    public float CompletionPercentage = 100;
    public float CompletionMin = 20;
    public abstract void Interact(Vector3 collisionPoint, Transform interactionObject);
}
