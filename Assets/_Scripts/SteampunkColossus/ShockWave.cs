using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public int shockwaveDamage = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().Damage(shockwaveDamage);
        }
    }
}
