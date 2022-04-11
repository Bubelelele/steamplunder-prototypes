using UnityEngine;

public class Pellet : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public ParticleSystem smoke;
    public GameObject shockwave;
    public float shockSpeed = 0.6f;
    public float shockRadius = 40f;


    private Animator bossAnim;
    private bool hit = false;
    private Transform target;

    private void Start()
    {
        bossAnim = GameObject.Find("BossAnimator").GetComponent<Animator>();
        target = GameManager.instance.player.transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        hit = true;
        smoke.Stop();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, projectileSpeed * Time.deltaTime);

        if (hit)
        {
            shockwave.transform.localScale = Vector3.Slerp(shockwave.transform.localScale, Vector3.one * shockRadius, shockSpeed * Time.deltaTime);
        }
        if (shockwave.transform.localScale.x == shockRadius)
        {
            bossAnim.SetBool("Shoot", false);
            Destroy(gameObject);
        }
    }
}
