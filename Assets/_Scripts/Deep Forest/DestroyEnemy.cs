using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public GameObject door;

    private void OnDestroy()
    {
        door.GetComponent<DoorToDungeon>().KilledAnotherOne();
    }
}
