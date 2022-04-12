using UnityEngine;

public class FootOff : MonoBehaviour, IInteractable
{
    public GameObject left;
    public GameObject right;

    public GameObject foot;
    public GameObject bossBody;
    public GameObject bossCart;
    public ParticleSystem explotion;

    private bool isOff = false;

    public string GetDescription()
    {
        return "Detach foot";
    }

    public void Interact()
    {
        if (!isOff)
        {
            left.SetActive(false);
            right.SetActive(false);
            explotion.Play();
            bossBody.GetComponent<SC_Stats>().Damage(500);
            bossBody.GetComponent<SC_AttackScript>().FootOff();
            foot.transform.parent = null;
            foot.AddComponent<Rigidbody>();
            foot.GetComponent<BoxCollider>().enabled = true;
            foot.GetComponent<Rigidbody>().isKinematic = false;
            foot.GetComponent<Rigidbody>().useGravity = true;
            foot.transform.position = new Vector3(foot.transform.position.x, foot.transform.position.y + 2, foot.transform.position.z);
            foot.GetComponent<Rigidbody>().AddRelativeForce(foot.transform.up * 10f, ForceMode.Impulse);
            foot.GetComponent<Rigidbody>().AddRelativeTorque(foot.transform.up * 10f, ForceMode.Impulse);
            gameObject.GetComponent<Collider>().enabled = false;
            isOff = true;
            Invoke("Hide", 2f);
        }

    }
    public void StopInteract()
    {
        //Not used
    }
    private void Hide()
    {
        foot.SetActive(false);
    }
}
