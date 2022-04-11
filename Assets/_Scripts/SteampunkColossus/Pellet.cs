using UnityEngine;

public class Pellet : MonoBehaviour
{
    public float projectileSpeed = 2f;
    public ParticleSystem smoke;
    public GameObject shockwave;
    public float shockSpeed = 5f;
    public float shockRadius = 20f;
    public bool hit = false;

    private Animator bossAnim;


    private void Start()
    {
        bossAnim = GameObject.Find("BossAnimator").GetComponent<Animator>();
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 10f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        hit = true;
        smoke.Stop();
    }
    private void Update()
    {
        if (hit)
        {
            shockwave.transform.localScale = Vector3.Lerp(shockwave.transform.localScale, Vector3.one * shockRadius, shockSpeed * Time.deltaTime);
        }
        if (shockwave.transform.localScale.x >= shockRadius-2f)
        {
            bossAnim.SetBool("Shoot", false);
            Destroy(gameObject);
        }
    }
}
