using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    [HideInInspector] public bool lookAtPlayer = false;

    [SerializeField] private float damping = 20;

    private void Update()
    {
        if (lookAtPlayer)
        {
            var targetRotation = Quaternion.LookRotation(new Vector3(GameManager.instance.player.transform.position.x, transform.position.y, GameManager.instance.player.transform.position.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);
        }
    }

    public void LookAtPlayer() { lookAtPlayer = true; }
    public void DontLookAtPlayer() { lookAtPlayer = false; }

}
