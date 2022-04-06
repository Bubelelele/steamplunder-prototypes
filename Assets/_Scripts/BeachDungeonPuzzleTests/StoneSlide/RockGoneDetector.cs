using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGoneDetector : MonoBehaviour
{
    public GameObject rock, smallRocks;

    public Animator[] anims;

    void Update()
    {
        if (!rock.activeSelf)
        {
            smallRocks.SetActive(true);
            StartCoroutine(puzzleDone());
        }
    }

    private IEnumerator puzzleDone()
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].SetBool("LiftUp", true);
            yield return new WaitForSeconds(1f);
        }
    }
}
