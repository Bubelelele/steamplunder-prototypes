using UnityEngine;

public class SC_Movement : MonoBehaviour
{
    public float movementSpeed = 4f;
    public float FOV = 80f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bossBody;
    [SerializeField] private float damping = 20;
    [SerializeField] private float closestDistance = 5f;
    [SerializeField] private float furthestDistance = 9f;

    [SerializeField] private Animator bossAnim;


    private bool walkToPlayer = true;
    private bool lookAtPlayer = true;
    private bool step = false;
    private bool spin = false;

    private void Update()
    {
        if (bossBody != null && bossBody.GetComponent<SC_Stats>().isActive)
        {
            if (lookAtPlayer && spin)
            {
                var targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);
            }

            if (Vector3.Distance(gameObject.transform.position, player.transform.position) > closestDistance && walkToPlayer)
            {

                if (step)
                {
                    //Moving towards the player
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), movementSpeed * Time.deltaTime);
                }
            }

            if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= closestDistance)
            {
                DontWalkToPlayer();
            }
            else if (Vector3.Distance(gameObject.transform.position, player.transform.position) > furthestDistance)
            {
                WalkToPlayer();
            }

            if (Vector3.Angle(transform.forward, player.transform.position - transform.position) > FOV/2 && !walkToPlayer)
            {
                LookAtPlayer();
                if(Vector3.Angle(transform.right, player.transform.position - transform.position) > 90)
                {
                    bossAnim.SetBool("PivotLeft", true);
                    bossAnim.SetBool("PivotRight", false);
                }
                else
                {
                    bossAnim.SetBool("PivotLeft", false);
                    bossAnim.SetBool("PivotRight", true);
                }
            }
            else if (Vector3.Angle(transform.forward, player.transform.position - transform.position) <= 7 && !walkToPlayer)
            {
                DontLookAtPlayer();
                bossAnim.SetBool("PivotLeft", false);
                bossAnim.SetBool("PivotRight", false);
            }
            
        }
    }
    public void WalkToPlayer()
    {
        walkToPlayer = true;
        bossAnim.SetBool("IsWalking", true);
        LookAtPlayer();
        Spin();
    }
    public void DontWalkToPlayer()
    { 
        walkToPlayer = false;
        bossAnim.SetBool("IsWalking", false);
    }
    public void Step() { step = true; }
    public void NoStep() { step = false; }
    public void Spin() { spin = true; }
    public void NoSpin() { spin = false; }

    public void LookAtPlayer(){ lookAtPlayer = true;}
    public void DontLookAtPlayer(){ lookAtPlayer = false;}
}
