using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGunPlatform : MonoBehaviour
{
    public float moveSpeed;
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
        while (other.CompareTag("Player"))
        {
            transform.position += transform.forward * moveSpeed;
        }
    }
}
