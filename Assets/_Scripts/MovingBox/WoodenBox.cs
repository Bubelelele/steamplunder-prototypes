using UnityEngine;

public class WoodenBox : MonoBehaviour, IInteractable
{
    
    public float timeToMove = 10;
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform targetTransform;
    public AudioSource woodBoxAudioSource;

    [HideInInspector] public int moveDirection = 0;
    [HideInInspector] public bool topEdge = false;
    [HideInInspector] public bool bottomEdge = false;
    [HideInInspector] public bool leftEdge = false;
    [HideInInspector] public bool rightEdge = false;


    //The movement
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, Time.deltaTime * timeToMove);      
    }

    public string GetDescription()
    {
        return "Push";
    }

    public void Interact()
    {
            if (moveDirection == 1 && !topEdge)
            {
                MoveUp();
                woodBoxAudioSource.Play();
            }
            else if (moveDirection == 2 && !bottomEdge)
            {
                MoveDown();
                woodBoxAudioSource.Play();
            }
            else if (moveDirection == 3 && !leftEdge)
            {
                MoveLeft();
                woodBoxAudioSource.Play();   
            }
            else if (moveDirection == 4 && !rightEdge)
            {
                MoveRight();
                woodBoxAudioSource.Play();
            }

    }
    public void StopInteract()
    {
        //Not used
    }


    //Functions to change the direction of the movement
    public void UpDir() {moveDirection = 1;}
    public void DownDir() {moveDirection = 2;}
    public void LeftDir() {moveDirection = 3;}
    public void RightDir() {moveDirection = 4;}

    //Move in a direction
    public void MoveUp() { targetTransform.position = new Vector3(transform.position.x,transform.position.y, transform.position.z + transform.localScale.z); }
    public void MoveDown() { targetTransform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - transform.localScale.z); }
    public void MoveLeft() { targetTransform.position = new Vector3(transform.position.x - transform.localScale.x, transform.position.y, transform.position.z); }
    public void MoveRight() { targetTransform.position = new Vector3(transform.position.x + transform.localScale.x, transform.position.y, transform.position.z); }

    //Functions to see if you've reached and edge
    public void TopEdge() { topEdge = true; }
    public void NotTopEdge() { topEdge = false; }

    public void BottomEdge() { bottomEdge = true; }
    public void NotBottomEdge() { bottomEdge = false; }

    public void LeftEdge() { leftEdge = true; }
    public void NotLeftEdge() { leftEdge = false; }

    public void RightEdge() { rightEdge = true; }
    public void NotRightEdge() { rightEdge = false; }



}
