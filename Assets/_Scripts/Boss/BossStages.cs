using UnityEngine;
using UnityEngine.AI;
public class BossStages : MonoBehaviour
{

    [HideInInspector] public bool secondStage = false;
    [HideInInspector] public bool thirdStage = false;

    [SerializeField] private GameObject boss;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform[] bossLocation;
    [SerializeField] private Transform targetLocation;
    [SerializeField] private Animator exitDoorAnimator;
    [SerializeField] private Animator stage2DoorAnimator;

    public void Stage2()
    {
        secondStage = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        boss.GetComponent<BossStats>().DeactivateBoss();
    }
    private void Update()
    {
        if (secondStage)
        {
            targetLocation.position = bossLocation[0].position;
            agent.SetDestination(targetLocation.position);

            exitDoorAnimator.SetBool("OpenDoor", true);
            stage2DoorAnimator.SetBool("OpenDoor", true);

            if (Vector3.Distance(transform.position, targetLocation.position) < 1)
            {
                boss.GetComponent<Animator>().SetTrigger("SecondStage");
                secondStage = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                stage2DoorAnimator.SetBool("OpenDoor", false);
                boss.GetComponent<AttackScript>().LastStage();
            }
        }
    }


}
