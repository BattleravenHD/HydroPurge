using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slime : Cleanable_Base
{
    [SerializeField] float reductionAmount = 0.95f;
    [SerializeField] ParticleSystem cleanExplosion;
    Material mat;

    Vector3 scale;
    bool disovling = false;

    private void Start()
    {
        scale = transform.localScale;
        mat = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        transform.localScale = scale;
        mat.SetFloat("_GlowPower", 0);
    }

    public override void Interact(Vector3 collisionPoint, Transform interactionObject)
    {
        if (!disovling)
        {
            mat.SetFloat("_GlowPower", 0.1f);

            transform.localScale = scale * 1.01f;

            CompletionPercentage -= reductionAmount;

            if (CompletionPercentage < CompletionMin)
            {
                disovling = true;
                transform.DOScale(Vector3.zero, 2);
                Destroy(gameObject.transform.parent.gameObject, 2.01f);
                cleanExplosion.Play();
            }
        }
    }
}
