using UnityEngine;

public class PushBackPlayer : MonoBehaviour
{
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GameManager.instance.player.GetComponent<Rigidbody>();
    }
    public void PushBack(float pushBackForce)
    {
        Vector3 recoilForceVector = (GameManager.instance.player.transform.position - gameObject.transform.position).normalized * pushBackForce;
        _rb.AddForce(recoilForceVector, ForceMode.Impulse);
    }

}
