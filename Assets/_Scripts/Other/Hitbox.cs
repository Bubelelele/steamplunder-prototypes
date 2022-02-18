using UnityEngine;

public class Hitbox : MonoBehaviour {
    public delegate void HitboxHit(IDamageable damageable);

    public event HitboxHit onHitboxHit;

    private void OnTriggerEnter(Collider other) {
        var damageable = other.transform.GetComponent<IDamageable>();
        if (damageable != null) onHitboxHit?.Invoke(damageable);
    }
}