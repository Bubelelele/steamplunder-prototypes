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
            Debug.Log("Yup");
            chair.AddComponent<Rigidbody>();
            chair.GetComponent<Rigidbody>().isKinematic = false;
            chair.GetComponent<Rigidbody>().useGravity = true;
            Vector3 recoilForceVector = (new Vector3(transform.position.x, transform.position.y + 3, transform.position.z) - other.transform.position).normalized * 15f;
            chair.GetComponent<Rigidbody>().AddForce(recoilForceVector, ForceMode.Impulse);
        }
        else if (isHit && other.gameObject.tag != "Boss" && other.gameObject.tag != "Player")
        {
            Destroy(chair);
            Debug.Log("Boom");
        }
    }
}
