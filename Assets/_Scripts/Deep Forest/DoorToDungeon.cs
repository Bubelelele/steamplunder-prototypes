using UnityEngine;

public class DoorToDungeon : MonoBehaviour
{
    private int enemiesKilled = 0;
    void Update()
    {
        if (enemiesKilled == 6)
        {
            gameObject.GetComponent<Animator>().SetTrigger("OpenDoor");
        }
    }
    public void KilledAnotherOne()
    {
        enemiesKilled++;
    }
}
