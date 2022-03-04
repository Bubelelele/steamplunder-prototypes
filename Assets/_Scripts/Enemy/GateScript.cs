using UnityEngine;

public class GateScript : MonoBehaviour
{
    public int enemiesKilled = 0;

    // Update is called once per frame
    void Update()
    {
        if (enemiesKilled == 3)
        {
            gameObject.GetComponent<Animator>().SetBool("GateDown", true);
        }
    }

    public void KilledAnotherOne()
    {
        enemiesKilled++;
    }
}
