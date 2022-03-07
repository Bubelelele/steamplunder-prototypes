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

    public void Stage2()
    {
        secondStage = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        boss.GetComponent<BossStats>().DeactivateBoss();
    }
    public void Stage3()
    {
        thirdStage = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        boss.GetComponent<BossStats>().DeactivateBoss();
    }
    private void Update()
    {
        if (secondStage)
        {
            targetLocation.position = bossLocation[0].position;
            agent.SetDestination(targetLocation.position);
            

            if(Vector3.Distance(transform.position, targetLocation.position) < 1)
            {
                secondStage = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
        if (thirdStage)
        {
            targetLocation.position = bossLocation[1].position;
            agent.SetDestination(targetLocation.position);

            if (Vector3.Distance(transform.position, targetLocation.position) < 1)
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                thirdStage = false;
            }
        }
    }


}
