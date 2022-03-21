using UnityEngine;

public class BridgeOverScript : MonoBehaviour
{
    public int numberOfPlatesSteppedOn = 0;

    private void Update()
    {

        if (numberOfPlatesSteppedOn == 1)
        {
            gameObject.GetComponent<Animator>().SetBool("Bridge1", true);
            gameObject.GetComponent<Animator>().SetBool("Bridge2", false);
        }
        else if (numberOfPlatesSteppedOn == 2)
        {
            gameObject.GetComponent<Animator>().SetBool("Bridge2", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Bridge1", false);
            gameObject.GetComponent<Animator>().SetBool("Bridge2", false);
        }
    }
}
