using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderCorrectCell : CylinderPressurePlates
{
    // Start is called before the first frame update
    void Start()
    {
        Bean = GameManager.instance.player.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.W))
		{
            Bean.GetComponent<CapsuleCollider>().enabled = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Bean.GetComponent<CapsuleCollider>().enabled = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Bean.GetComponent<CapsuleCollider>().enabled = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Bean.GetComponent<CapsuleCollider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {


            Bean.GetComponent<CapsuleCollider>().enabled = false;
            Bean.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            

            Bean.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
