using UnityEngine;
using UnityEngine.Playables;

public class GearToWeaponPickup : MonoBehaviour {

    private GameObject gearTimeline;

    private void Start()
    {
        gearTimeline = GameObject.FindGameObjectWithTag("BossTimeline");
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            EffectManager.instance.PickupEffect(transform.position);
            AudioManager.instance?.Play("cogpickup");
            gearTimeline.GetComponent<PlayableDirector>().Play();
            Destroy(gameObject);
        }
    }
}