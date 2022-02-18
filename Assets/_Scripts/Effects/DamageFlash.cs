using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour {

    [SerializeField] private float flashTime = .15f;

    private MeshRenderer _renderer;
    private Color _origColor;

    private void Start() {
        _renderer = GetComponent<MeshRenderer>();
        _origColor = _renderer.material.color;
    }

    public void Flash() => StartCoroutine(FlashEffect());

    private IEnumerator FlashEffect() {
        _renderer.material.color = Color.white;
        yield return new WaitForSeconds(flashTime);
        _renderer.material.color = _origColor;
    }
}