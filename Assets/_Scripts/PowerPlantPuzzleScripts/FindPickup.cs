using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPickup : MonoBehaviour
{
    public FindCogMachine cogMachine;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cogMachine.GetCog();
            AudioManager.instance?.Play("cogpickup");
            Destroy(gameObject);
        }
    }


   
}