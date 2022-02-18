using UnityEngine;

public class SlideBox : MonoBehaviour
{
    public GameObject player;

    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform gridPosition;
    public Transform hitBoxTransform;

    [HideInInspector] public bool slidingUp;
    [HideInInspector] public bool slidingDown;
    [HideInInspector] public bool slidingLeft;
    [HideInInspector] public bool slidingRight;
    [HideInInspector] public bool canSlide;
    [HideInInspector] public bool isSliding;
     public bool hitAnotherBox = false;

    private bool attackStarted = false;
    private bool doneSlidng1;
    public bool doneSlidng2;

    void Update()
    {
        //When the box is sliding in a direction
        if (slidingUp)
        {
            gameObject.GetComponent<WoodenBox>().targetTransform.position = up.position;
        }
        else if (slidingDown)
        {
            gameObject.GetComponent<WoodenBox>().targetTransform.position = down.position;
        }
        else if (slidingLeft)
        {
            gameObject.GetComponent<WoodenBox>().targetTransform.position = left.position;
        }
        else if (slidingRight)
        {
            gameObject.GetComponent<WoodenBox>().targetTransform.position = right.position;
        }
        else
        {

        //When the box reaches the edge 
            if (!doneSlidng1 && !hitAnotherBox)
            {
                transform.position = new Vector3(gridPosition.position.x, transform.position.y, gridPosition.position.z);
                doneSlidng1 = true;
            }
            else if(!doneSlidng1 && hitAnotherBox)
            {
                if(gameObject.GetComponent<WoodenBox>().moveDirection == 1)
                {
                    transform.position = new Vector3(hitBoxTransform.position.x, transform.position.y, hitBoxTransform.position.z - transform.localScale.z);
                }
                else if (gameObject.GetComponent<WoodenBox>().moveDirection == 2)
                {
                    transform.position = new Vector3(hitBoxTransform.position.x, transform.position.y, hitBoxTransform.position.z + transform.localScale.z);
                }
                else if (gameObject.GetComponent<WoodenBox>().moveDirection == 3)
                {
                    transform.position = new Vector3(hitBoxTransform.position.x + transform.localScale.x, transform.position.y, hitBoxTransform.position.z);
                }
                else if (gameObject.GetComponent<WoodenBox>().moveDirection == 4)
                {
                    transform.position = new Vector3(hitBoxTransform.position.x - transform.localScale.x, transform.position.y, hitBoxTransform.position.z);
                }

                doneSlidng1 = true;
                hitAnotherBox = false;
            }
            if (doneSlidng1 && !doneSlidng2)
            {
                
                gameObject.GetComponent<WoodenBox>().targetTransform.position = transform.position;
                doneSlidng2 = true;
                isSliding = false;
            }
        }
        
        //Checks if you can slide in a direction
        if (gameObject.GetComponent<WoodenBox>().topEdge) {slidingUp = false;}
        if (gameObject.GetComponent<WoodenBox>().bottomEdge) {slidingDown = false;}
        if (gameObject.GetComponent<WoodenBox>().leftEdge) {slidingLeft = false;}
        if (gameObject.GetComponent<WoodenBox>().rightEdge) {slidingRight = false;}



        //Checks if you are trying to move the box
        if (canSlide && Input.GetKeyDown(KeyCode.F) && !attackStarted && GameManager.instance.player.GetComponent<GearManager>().no)
        {
            attackStarted = true;
            if (!gameObject.GetComponent<WoodenBox>().topEdge && gameObject.GetComponent<WoodenBox>().moveDirection == 1)
            {
                Invoke("HammerAnimationUp", 0.3f);
            }
            else if (!gameObject.GetComponent<WoodenBox>().bottomEdge && gameObject.GetComponent<WoodenBox>().moveDirection == 2)
            {
                Invoke("HammerAnimationDown", 0.3f);
            }
            else if (!gameObject.GetComponent<WoodenBox>().leftEdge && gameObject.GetComponent<WoodenBox>().moveDirection == 3)
            {
                Invoke("HammerAnimationLeft", 0.3f);
            }
            else if (!gameObject.GetComponent<WoodenBox>().rightEdge && gameObject.GetComponent<WoodenBox>().moveDirection == 4 )
            {
                Invoke("HammerAnimationRight", 0.3f);
            }
        }
        if (GameManager.instance.player.gameObject.GetComponent<HammerController>().CanAttack)
        {
            attackStarted = false;
        }

    }
    public void CanSlide() { canSlide = true; }
    public void CannotSlide() { canSlide = false; }

    public void HitAnotherBox() { hitAnotherBox = true; }

    private void HammerAnimationUp() 
    {
        doneSlidng1 = false;
        doneSlidng2 = false;
        isSliding = true;
        slidingUp = true;
    }
    private void HammerAnimationDown()
    {
        doneSlidng1 = false;
        doneSlidng2 = false;
        isSliding = true;
        slidingDown = true;
    }
    private void HammerAnimationLeft()
    {
        doneSlidng1 = false;
        doneSlidng2 = false;
        isSliding = true;
        slidingLeft = true;
    }
    private void HammerAnimationRight()
    {
        doneSlidng1 = false;
        doneSlidng2 = false;
        isSliding = true;
        slidingRight = true;
    }
}
