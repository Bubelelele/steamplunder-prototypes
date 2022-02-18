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

    public void AOE(Vector3 position, float diameter) {
        GameObject aoeEffect = Instantiate(hammerAoePrefab, position, Quaternion.identity);
        aoeEffect.transform.localScale = Vector3.one * diameter;
    }

    public void BloodSplat(Vector3 position) => Instantiate(bloodSplatPrefab, position, Quaternion.identity);
    public void DeathEffect(Vector3 position) => Instantiate(deathEffectPrefab, position, Quaternion.identity);

}
