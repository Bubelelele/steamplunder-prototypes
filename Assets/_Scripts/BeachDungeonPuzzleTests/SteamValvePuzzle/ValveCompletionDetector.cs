using UnityEngine;
using UnityEngine.Events;

public class ValveCompletionDetector : MonoBehaviour
{
    public ValveInteract[] valveList;

    [SerializeField]private bool[] isComplete;
    [SerializeField] private UnityEvent onActivate;

    private void Awake()
    {
        GetComponent<Renderer>().material.color = Color.red;
        isComplete = new bool[valveList.Length];
    }

    private void Update()
    {
        // checks if the pipes are "on" or "off" and records it in isComplete
        for (int i = 0; i < valveList.Length; i++)
        {
           isComplete[i] = !valveList[i].GetSteamActive();
        }
       
        //changes the color of the square to green when it is complete and keeps it at red if it isn't
       if (IsThePuzzleComplete())
        {
            GetComponent<Renderer>().material.color = Color.green;
            onActivate.Invoke();
            Debug.Log("lower cage");
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private bool IsThePuzzleComplete()
    {
        for (int i = 0; i < isComplete.Length; i++)
        {
            if (isComplete[i] == false)
            {
                return false;
            }
        }
        return true;
    }
}
