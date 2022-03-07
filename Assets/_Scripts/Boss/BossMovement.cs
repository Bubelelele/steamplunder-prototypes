using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float movementSpeed = 3f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject detectionTrigger;
    
    
    private bool walkToPlayer = true;
    

    void Update()
    {
        if(boss.GetComponent<BossStats>().isActive)
        {
            transform.LookAt(new Vector3(player.transform.position.x, boss.transform.position.y, player.transform.position.z));
            if (!detectionTrigger.GetComponent<BossDetectionTrigger>().attackRange && walkToPlayer)
            {
                //Moving towards the player
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, boss.transform.position.y, player.transform.position.z), movementSpeed * Time.deltaTime);
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
}


