using UnityEngine;

public class ChairDestruction : MonoBehaviour
{
    public GameObject chair;
    private bool isHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss" && !isHit)
        {
            isHit = true;
            chair.AddComponent<Rigidbody>();
            chair.GetComponent<Rigidbody>().isKinematic = false;
            chair.GetComponent<Rigidbody>().useGravity = true;
            Vector3 recoilForceVector = (new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) - other.transform.position).normalized * 20f;
            chair.GetComponent<Rigidbody>().AddForce(recoilForceVector, ForceMode.Impulse);
            chair.GetComponent<Rigidbody>().AddTorque(recoilForceVector, ForceMode.Impulse);
        }
        else if (isHit && other.gameObject.tag != "Boss" && other.gameObject.tag != "Player")
        {
            Destroy(chair, 1f);
        }
    }
}
