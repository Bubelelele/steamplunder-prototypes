using UnityEngine;

public class CogPickup : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            EffectManager.instance.PickupEffect(transform.position);
            collision.gameObject.GetComponent<PlayerStats>()?.AddCogs(1);
            AudioManager.instance.Play("cogpickup");
            Destroy(gameObject);
        }
    }
}