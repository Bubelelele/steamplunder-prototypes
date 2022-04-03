using UnityEngine;

public class SC_Movement : MonoBehaviour
{
    public float movementSpeed = 4f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bossBody;
    [SerializeField] private float damping = 20;
    [SerializeField] private float closestDistance = 5f;
    [SerializeField] private float furthestDistance = 9f;


    private bool walkToPlayer = true;
    private bool lookAtPlayer = true;

    private void Update()
    {
        if (bossBody != null && bossBody.GetComponent<SC_Stats>().isActive && lookAtPlayer)
        {

            var targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);

            if (Vector3.Distance(gameObject.transform.position, player.transform.position) > closestDistance && walkToPlayer)
            {
                //Moving towards the player
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), movementSpeed * Time.deltaTime);
            }

            if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= closestDistance)
            {
                DontWalkToPlayer();
            }
            else if (Vector3.Distance(gameObject.transform.position, player.transform.position) > furthestDistance)
            {
                WalkToPlayer();
            }
        }
    }
    public void WalkToPlayer(){ walkToPlayer = true;}
    public void DontWalkToPlayer(){ walkToPlayer = false;}
}
