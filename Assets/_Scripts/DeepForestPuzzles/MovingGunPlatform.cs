using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGunPlatform : MonoBehaviour
{
    public float moveSpeed;
    public float driftSpeed;
    public float jumpSpeed;
    public bool driftLeft;

    private bool playerOn;
    Vector3 startPosition;

    public GameObject walls;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (playerOn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                walls.SetActive(true);
                Invoke("UnWall", 3f);
            }
        }
        
    }
    

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            playerOn = true;
            transform.position += transform.forward * moveSpeed;
            if (driftLeft)
                transform.position += -transform.right * driftSpeed;
            else
                transform.position += transform.right * driftSpeed;

           /* if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                walls.SetActive(true);
                Debug.Log("get walles");
                Invoke("UnWall", 3f);
            }*/
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            GameManager.instance.player.gameObject.transform.parent = transform;
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            GameManager.instance.player.gameObject.transform.parent = null;
        }

        if (other.CompareTag("Player"))
        {
            playerOn = false;
            transform.position = startPosition;
        }
    }

    private void UnWall()
    {
        walls.SetActive(false);
    }

    public void LeftWallShot()
    {
        //go right
        transform.position += transform.right * jumpSpeed;
    }

    public void RightWallShot()
    {
        //go left
        transform.position += -transform.right * jumpSpeed;
    }

    public void BottomWallShot()
    {
        //go forward
        transform.position += transform.forward * jumpSpeed;
    }

    public void TopWallShot()
    {
        //go back
        transform.position += -transform.forward * jumpSpeed;
    }
}
