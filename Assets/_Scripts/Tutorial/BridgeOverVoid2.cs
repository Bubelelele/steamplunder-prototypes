using UnityEngine;

public class BridgeOverVoid2 : MonoBehaviour
{
    public GameObject bridgeOverVoid;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            bridgeOverVoid.GetComponent<BridgeOverScript>().numberOfPlatesSteppedOn++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            bridgeOverVoid.GetComponent<BridgeOverScript>().numberOfPlatesSteppedOn--;
        }
    }
}
