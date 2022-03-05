using UnityEngine;

public class BossMovement : MonoBehaviour
{

    [HideInInspector] public bool secondStage = false;
    [HideInInspector] public bool thirdStage = false;

    [SerializeField] private Transform[] bossLocation;

    public void Stage2()
    {
        NewStage(bossLocation[0]);
        secondStage = true;
    }
    public void Stage3()
    {
        NewStage(bossLocation[1]);
        thirdStage = true;
    }
    private void NewStage(Transform location)
    {
        gameObject.transform.position = location.position;
    }
}
