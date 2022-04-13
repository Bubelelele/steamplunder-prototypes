using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderPressurePlates : MonoBehaviour
{
    public GameObject Bean;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
            Bean.GetComponent<CapsuleCollider>().isTrigger = true;
            Bean.GetComponent<Rigidbody>().isKinematic = true;
        }
		
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
            Bean.GetComponent<CapsuleCollider>().isTrigger = false;
            Bean.GetComponent<Rigidbody>().isKinematic = false;
        }
	}
}
