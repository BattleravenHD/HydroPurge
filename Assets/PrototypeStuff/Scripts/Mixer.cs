using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mixer : MonoBehaviour
{
    [SerializeField] Water_Gun_Controller player;
    [SerializeField] ClenableScriptable glueBall;
    [SerializeField] ClenableScriptable acidBall;
    [SerializeField] MeshRenderer glueSymbol;
    [SerializeField] MeshRenderer acidSymbol;
    [SerializeField] Transform shrinkLocation;

    [SerializeField] private AudioSource gunAudioSource;
    [SerializeField] private AudioClip mixerSoundEffect;

    bool hasGlue = false;
    bool hasBinder = false;
    bool hasAcid = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            return;
        }
        if (other.TryGetComponent<CraftingMat>(out CraftingMat craftingMat))
        {
            gunAudioSource.PlayOneShot(mixerSoundEffect);
            switch (craftingMat.craftingMater)
            {
                case CraftingMaterial.Coal:
                    hasGlue = true;
                    if (!hasBinder)
                    {
                        glueSymbol.material.SetColor("_BaseColor", new Color(1, 0.549f, 0));
                        glueSymbol.material.SetColor("_EmissionColor", new Color(1, 0.549f, 0));
                    }
                    else
                    {
                        glueSymbol.material.SetColor("_BaseColor", Color.green);
                        glueSymbol.material.SetColor("_EmissionColor", Color.green);
                    }
                    break;
                case CraftingMaterial.Seaweed:
                    hasBinder = true;
                    if (!hasGlue)
                    {
                        glueSymbol.material.SetColor("_BaseColor", new Color(1, 0.549f, 0));
                        glueSymbol.material.SetColor("_EmissionColor", new Color(1, 0.549f, 0));
                    }
                    else
                    { 
                        glueSymbol.material.SetColor("_BaseColor", Color.green);
                        glueSymbol.material.SetColor("_EmissionColor", Color.green);
                        
                    }
                    if (!hasAcid)
                    {
                        acidSymbol.material.SetColor("_BaseColor", new Color(1, 0.549f, 0));
                        acidSymbol.material.SetColor("_EmissionColor", new Color(1, 0.549f, 0));
                    }
                    else
                    {
                        acidSymbol.material.SetColor("_BaseColor", Color.green);
                        acidSymbol.material.SetColor("_EmissionColor", Color.green);
                    }
                    break;
                case CraftingMaterial.Rock:
                    hasAcid = true;
                    if (!hasBinder)
                    {
                        acidSymbol.material.SetColor("_BaseColor", new Color(1, 0.549f, 0));
                        acidSymbol.material.SetColor("_EmissionColor", new Color(1, 0.549f, 0));
                    }
                    else
                    {
                        acidSymbol.material.SetColor("_BaseColor", Color.green);
                        acidSymbol.material.SetColor("_EmissionColor", Color.green);
                    }
                    break;
                default:
                    break;
            }
        }

        if (hasAcid && hasBinder)
        {
            if (!player.substanceBalls.Contains(acidBall))
            {
                player.substanceBalls.Add(acidBall);
            }
        }

        if (hasBinder && hasGlue)
        {
            if (!player.substanceBalls.Contains(glueBall))
            {
                player.substanceBalls.Add(glueBall);
            }
        }

        other.transform.SetParent(shrinkLocation);
        other.GetComponent<Collider>().enabled = false;

        other.transform.DOLocalMove(Vector3.zero, 2);
        other.transform.DOLocalRotate(new Vector3(Random.Range(0, 361), Random.Range(0, 361), Random.Range(0, 361)), 2);
        other.transform.DOScale(Vector3.zero, 2);

        Destroy(other.gameObject, 2.1f);
    }
}
