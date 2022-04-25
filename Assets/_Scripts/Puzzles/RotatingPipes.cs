using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPipes : MonoBehaviour
{
    private float speed = 0.5f;
    public GameObject Pipe1;
    public GameObject Pipe2;
    public GameObject Pipe3;

    private float RotationResetSpeed1 = 0.05f;
    private float RotationResetSpeed2 = 0.1f;
    private float RotationResetSpeed3 = 0.15f;

    private bool firstPipe = false;
    

    public Quaternion OriginalRotationValue;

    // Start is called before the first frame update
    private void Awake()
    {
        OriginalRotationValue = transform.rotation;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (firstPipe)
        {
            Pipe1.transform.Rotate(Vector3.forward, speed);
        }
        if (firstPipe == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, OriginalRotationValue, Time.deltaTime * RotationResetSpeed1);
        }
        
    }

    public void PipeOne()
    {
        if (Input.GetKey(KeyCode.E))
        {
            firstPipe = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            firstPipe = false;
        }
     
    }

    public void PipeTwo()
    {
        if (Input.GetKey(KeyCode.E))
        {

            Pipe2.transform.Rotate(Vector3.back, speed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, OriginalRotationValue, Time.deltaTime * RotationResetSpeed2);
        }
    }

    public void PipeThree()
    {

        if (Input.GetKey(KeyCode.E))
        {

            Pipe3.transform.Rotate(Vector3.forward, speed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, OriginalRotationValue, Time.deltaTime * RotationResetSpeed3);
        }
    }
}
