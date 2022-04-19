using UnityEngine;

public class CoverOff : MonoBehaviour
{
    public GameObject bossBody;
    public GameObject cover;
    public GameObject mouseTrigger;

    private bool entered = false;
    private bool done = false;
    private int startHealth;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!entered)
            {
                startHealth = bossBody.GetComponent<SC_Stats>()._health;
                entered = true;
            }

            if(startHealth > bossBody.GetComponent<SC_Stats>()._health)
            {
                if (!done)
                {
                    DragOff();
                    done = true;
                }

            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            entered = false;

        }
    }
    private void DragOff()
    {
        mouseTrigger.GetComponent<DoorOnBoss>().CanBeDraggedOff();
        cover.transform.parent = null;
        cover.AddComponent<BoxCollider>();
        cover.AddComponent<Rigidbody>();
        cover.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * -1f, ForceMode.Impulse);
        Invoke("Hide", 2f);
    }

    private void Hide()
    {
        cover.GetComponent<MeshRenderer>().enabled = false;
        Destroy(cover);
    }
}
