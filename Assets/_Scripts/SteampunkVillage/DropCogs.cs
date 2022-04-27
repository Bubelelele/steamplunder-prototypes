using System.Collections;
using UnityEngine;

public class DropCogs : MonoBehaviour
{
    public void Correct()
    {
            StartCoroutine(Reward());
    }

    private IEnumerator Reward()
    {
        for (int i = 0; i < 5; i++)
        {
            EffectManager.instance.CogPickup(gameObject.transform.position + Vector3.up * 2);
            yield return new WaitForSeconds(.5f);
        }
    }
}
