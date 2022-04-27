using UnityEngine;
using Cinemachine;

public class CinemachineTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera cam;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            cam.m_Priority = 12;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cam.m_Priority = 10;
        }
    }
}
