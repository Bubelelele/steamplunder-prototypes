using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {
    public static EffectManager instance;
    private void Awake() => instance = this;

    [Header("References (Prefabs)")]
    [SerializeField] private GameObject hammerAoePrefab;
    [SerializeField] private GameObject bloodSplatPrefab;
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private GameObject pickupEffectPrefab;
    [SerializeField] private GameObject cogPickupPrefab;
    [SerializeField] private GameObject sparkPrefab;

    public void AOE(Vector3 position, float diameter) {
        GameObject aoeEffect = Instantiate(hammerAoePrefab, position, Quaternion.identity);
        aoeEffect.transform.localScale = Vector3.one * diameter;
    }

    public void CogPickup(Vector3 position) {
        GameObject cog = Instantiate(cogPickupPrefab, position, Quaternion.identity);
        Rigidbody rb = cog.GetComponent<Rigidbody>();
        rb.AddForce(Random.Range(-3f, 3f), 4f, Random.Range(-3f, 3f), ForceMode.Impulse);
    }

    public void BloodSplat(Vector3 position) => Instantiate(bloodSplatPrefab, position, Quaternion.identity);
    public void DeathEffect(Vector3 position) => Instantiate(deathEffectPrefab, position, Quaternion.identity);
    public void PickupEffect(Vector3 position) => Instantiate(pickupEffectPrefab, position, Quaternion.identity);

}
