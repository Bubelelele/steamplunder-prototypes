using UnityEngine;

public class SC_Movement : MonoBehaviour
{
    public float movementSpeed = 4f;
    public float FOV = 80f;
    [HideInInspector] public bool playerOnLeftSide = false;

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
                if (bossBody.GetComponent<SC_AttackScript>().animationPlaying)
                {
                    DontLookAtPlayer();
                }
                else
                {
                    LookAtPlayer();
                }

                
                if (step)
                {
                    //Moving towards the player
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), movementSpeed * Time.deltaTime);
                    var targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);
                }
            }

            //Check if the boss should move to the player
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= closestDistance)
            {
                bossBody.GetComponent<SC_AttackScript>().CanAttack();
                DontWalkToPlayer();
                if (Vector3.Angle(transform.forward, player.transform.position - transform.position) <= 7)
                {
                    DontLookAtPlayer();
                }

            }
            else if (Vector3.Distance(gameObject.transform.position, player.transform.position) > furthestDistance)
            {
                WalkToPlayer();
                bossBody.GetComponent<SC_AttackScript>().CannotAttack();
            }

            //Check which side of the boss the player is on
            if (Vector3.Angle(transform.right, player.transform.position - transform.position) > 90){ playerOnLeftSide = true;}

            else{ playerOnLeftSide = false;}

            if (Vector3.Angle(transform.forward, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position) > 90 && Vector3.Distance(transform.position, player.transform.position) < 4f)
            {
                bossBody.GetComponent<SC_AttackScript>().Slam();
            }
            else if (Vector3.Angle(transform.forward, player.transform.position - transform.position) > FOV/2 && !walkToPlayer)
            {
                if (playerOnLeftSide)
                {

                    bossAnim.SetBool("PivotLeft", true);
                    bossAnim.SetBool("PivotRight", false);
                    LookAtPlayer();

                }
                else
                {

                    bossAnim.SetBool("PivotLeft", false);
                    bossAnim.SetBool("PivotRight", true);
                    LookAtPlayer();

                }
            }
            else if (Vector3.Angle(transform.forward, player.transform.position - transform.position) <= 30 && !walkToPlayer)
            {
                bossAnim.SetBool("PivotLeft", false);
                bossAnim.SetBool("PivotRight", false);
            }
            
        }
    }
    public void WalkToPlayer()
    {
        walkToPlayer = true;
        bossAnim.SetBool("IsWalking", true);
    }
    public void DontWalkToPlayer()
    { 
        walkToPlayer = false;
        NoStep();
        bossAnim.SetBool("IsWalking", false);
    }
    public void Step() { step = true; }
    public void NoStep() { step = false; }
    public void Spin() 
    { 
        spin = true;
        LookAtPlayer();
    }
    public void NoSpin() 
    { 
        spin = false;
        DontLookAtPlayer();
    }

    public void LookAtPlayer(){ lookAtPlayer = true;}
    public void DontLookAtPlayer(){ lookAtPlayer = false;}

    public void SetFOV(float newFOV)
    {
        FOV = newFOV;
    }
}
