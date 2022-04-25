using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPickupActiveCog : MonoBehaviour
{
    public GameObject visibleCog;

    public void SetPickupActive()
    {
        visibleCog.SetActive(!visibleCog.activeSelf);
    }
}
