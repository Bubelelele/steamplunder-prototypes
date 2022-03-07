using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float movementSpeed = 7f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject detectionTrigger;
    [SerializeField] private float damping = 20;
    
    
    private bool walkToPlayer = true;
    private bool lookAtPlayer = true;
    

    void Update()
    {
        if(boss.GetComponent<BossStats>().isActive && lookAtPlayer)
        {

            var targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);

            if (!detectionTrigger.GetComponent<BossDetectionTrigger>().attackRange && walkToPlayer)
            {
                //Moving towards the player
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), movementSpeed * Time.deltaTime);
            }
        }
    }
    public void WalkToPlayer()
    {
        walkToPlayer = true;
    }
    public void DontWalkToPlayer()
    {
        walkToPlayer = false;
    }
    public void LookAtPlayer()
    {
        lookAtPlayer = true;
    }
    public void DontLookAtPlayer()
    {
        lookAtPlayer = false;
    }
    public void NormalSpeed()
    {
        movementSpeed = 7f;
    }
    public void SwordSwingSpeed()
    {
        movementSpeed = 1.5f;
    }
    public void ChargeSpeed()
    {
        movementSpeed = 14f;
    }
}


