using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject detectionTrigger;

    void Update()
    {
        if(boss.GetComponent<BossStats>().isActive == true)
        {
            transform.LookAt(new Vector3(player.transform.position.x, boss.transform.position.y, player.transform.position.z));
            if (!detectionTrigger.GetComponent<BossDetectionTrigger>().canAttack)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, boss.transform.position.y, player.transform.position.z), movementSpeed * Time.deltaTime);
            }
            else
            {
                boss.GetComponent<AnimationScript>().Block();
                Invoke("DelayedAttack", 1f);
            }
        }
        if (boss.GetComponent<BossStats>().isActive == false)
        {
            Debug.Log("Leave player");
        }
    }
    private void DelayedAttack()
    {
        boss.GetComponent<AnimationScript>().Slash();
    }


}


