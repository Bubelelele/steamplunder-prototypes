using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool canUse = true;
    private bool directionBool = true;

    public GameObject[] SpinningCogs;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canUse)
        {
            IsUsed();
        }
    }

    private void IsUsed()
    {
        gameObject.GetComponent<Animator>().SetBool("Up", directionBool);
        directionBool = !directionBool;
        canUse = false;
        for (int i = 0; i < SpinningCogs.Length; i++)
        {
            SpinningCogs[i].GetComponent<Spin>().enabled = true;
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
