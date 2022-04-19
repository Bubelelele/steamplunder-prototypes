using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool canUse = true;
    private bool directionBool = true;

    public GameObject[] SpinningCogs;

    public void IsUsed()
    {
        if (canUse)
        {
            gameObject.GetComponent<Animator>().SetBool("Up", directionBool);
            directionBool = !directionBool;
            canUse = false;
            for (int i = 0; i < SpinningCogs.Length; i++)
            {
                SpinningCogs[i].GetComponent<Spin>().enabled = true;
            }
        }
    }
    private void Done()
    {
        canUse = true;
        for (int i = 0; i < SpinningCogs.Length; i++)
        {
            SpinningCogs[i].GetComponent<Spin>().enabled = false;
        }
    }
}
