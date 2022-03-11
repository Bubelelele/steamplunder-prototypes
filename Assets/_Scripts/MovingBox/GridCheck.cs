using UnityEngine;

public class GridCheck : MonoBehaviour
{
    public Transform gridPosition;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Grid")
        {
            gridPosition.position = other.transform.position;
        }

    }
}
