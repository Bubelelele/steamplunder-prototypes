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
                Spin();
                LookAtPlayer();
                if (step)
                {
                    //Moving towards the player
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), movementSpeed * Time.deltaTime);
                }
            }

            //Check if the boss should move to the player
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= closestDistance)
            {
                DontWalkToPlayer();
                if (Vector3.Angle(transform.forward, player.transform.position - transform.position) <= 7)
                {
                    NoSpin();
                    bossBody.GetComponent<SC_AttackScript>().CanAttack();
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


            if (Vector3.Angle(transform.forward, player.transform.position - transform.position) > FOV/2 && !walkToPlayer)
            {
                if (playerOnLeftSide)
                {

                    bossAnim.SetBool("PivotLeft", true);
                    bossAnim.SetBool("PivotRight", false);
                    LookAtPlayer();
                    bossBody.GetComponent<SC_AttackScript>().CannotAttack();
                }
                else
                {

                    bossAnim.SetBool("PivotLeft", false);
                    bossAnim.SetBool("PivotRight", true);
                    LookAtPlayer();
                    bossBody.GetComponent<SC_AttackScript>().CannotAttack();
                }
            }
            else if (Vector3.Angle(transform.forward, player.transform.position - transform.position) <= 7 && !walkToPlayer)
            {
                bossBody.GetComponent<SC_AttackScript>().CanAttack();
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
    }
    public void DontWalkToPlayer()
    { 
        walkToPlayer = false;
        NoStep();
        bossAnim.SetBool("IsWalking", false);
    }
    public void Step() { step = true; }
    public void NoStep() { step = false; }
    public void Spin() { spin = true; }
    public void NoSpin() { spin = false; }

    public void LookAtPlayer(){ lookAtPlayer = true;}
    public void DontLookAtPlayer(){ lookAtPlayer = false;}

    public void SetFOV(float newFOV)
    {
        FOV = newFOV;
    }
}
