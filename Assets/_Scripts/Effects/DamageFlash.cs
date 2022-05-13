using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour {

    [SerializeField] private float flashTime = .15f;
    [SerializeField] private Renderer meshRenderer;
    
    private Color _origColor;

    private void Start() {
        _origColor = meshRenderer.material.color;
    }

    public void Flash() => StartCoroutine(FlashEffect());

    private IEnumerator FlashEffect() {
        Material[] materials = meshRenderer.materials;
        foreach (Material material in materials) {
            material.color = Color.white;
        }

        meshRenderer.materials = materials;
        yield return new WaitForSeconds(flashTime);
        foreach (Material material in materials) {
            material.color = _origColor;
        }

        meshRenderer.materials = materials;
    }
}