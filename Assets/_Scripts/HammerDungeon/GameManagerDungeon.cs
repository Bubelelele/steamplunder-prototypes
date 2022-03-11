using UnityEngine;

public class GameManagerDungeon : MonoBehaviour
{

    public Animator doorAnim;
    public Animator doorOutAnim;
    public Animator gridLiftAnim;


    void Update()
    {
        //Door
        if (Input.GetKeyDown(KeyCode.I))
        {
            doorAnim.SetBool("DoorDown", true);
            doorOutAnim.SetBool("DoorDown", true);
        }


        //Grid Lift
        if (Input.GetKeyDown(KeyCode.U))
        {
            gridLiftAnim.SetBool("LiftGrid", true);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            gridLiftAnim.SetBool("LiftGrid", false);
        }


    }
}
