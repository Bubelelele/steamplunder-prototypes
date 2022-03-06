using UnityEngine;

public class BossStages : MonoBehaviour
{

    [HideInInspector] public bool secondStage = false;
    [HideInInspector] public bool thirdStage = false;

    [SerializeField] private Transform[] bossLocation;
    [SerializeField] private Transform targetLocation;
    [SerializeField] private float timeForBossToMove = 20f;

    public void Stage2()
    {
        secondStage = true;
        gameObject.GetComponent<BossStats>().DeactivateBoss();
    }
    public void Stage3()
    {
        thirdStage = true;
        gameObject.GetComponent<BossStats>().DeactivateBoss();
    }
    private void Update()
    {
        if (secondStage)
        {
            targetLocation.position = bossLocation[0].position;
            gameObject.transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, timeForBossToMove * Time.deltaTime);
            
            if(transform.position == targetLocation.position)
            {
                secondStage = false;
            }
        }
        if (thirdStage)
        {
            targetLocation.position = bossLocation[1].position;
            gameObject.transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, timeForBossToMove * Time.deltaTime);
            
            if (transform.position == targetLocation.position)
            {
                thirdStage = false;
            }
        }
    }


}
