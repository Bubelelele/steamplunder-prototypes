using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject detectionTrigger;
    [SerializeField] private GameObject animationController;

    void Update()
    {
        if(gameObject.GetComponent<BossStats>().isActive == true)
        {
            transform.LookAt(player.transform);
            if (!detectionTrigger.GetComponent<BossDetectionTrigger>().canAttack)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z), movementSpeed * Time.deltaTime);
                animationController.GetComponent<AnimationScript>().Block();
            }
            else
            {
                animationController.GetComponent<AnimationScript>().Slash();
            }
        }
        if (gameObject.GetComponent<BossStats>().isActive == false)
        {
            Debug.Log("Leave player");
        }
    }



}


