using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRoom : MonoBehaviour
{
    [SerializeField] GameObject drip;
    [SerializeField] GameObject[] doors;
    [SerializeField] GameObject[] water;

    bool moved = false;

    private void Update()
    {
        if (drip == null && !moved)
        {
            moved = true;
            StartCoroutine(wait());

            foreach (GameObject item in water)
            {
                item.transform.DOLocalMoveY(0, 3);
            }
            
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        foreach (GameObject item in doors)
        {
            float location = item.transform.localPosition.y + 4;
            item.transform.DOLocalMoveY(location, 2);
        }
    }
}
