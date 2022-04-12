using UnityEngine;

public class PelletGrapple : MonoBehaviour
{
    [HideInInspector] public bool canHurtBoss;
    public Canvas grappleUI;
    public Transform rotatePelletParent;
    public float rotationSpeed = 100f;

    private bool posIsUpdated;
    private bool rotateNow = false;
    private bool isLeftSide = false;
    private Vector3 startRot;
    private GameObject bossCart;

    private void Start()
    {
        canHurtBoss = false;
        rotatePelletParent = GameObject.Find("RotatePelletParent").transform;
        bossCart = GameObject.Find("BossCart");
        posIsUpdated = false;
    }

    private void OnMouseOver()
    {
        if (!gameObject.GetComponent<Pellet>().hit)
        {
            grappleUI.enabled = true;
            if (Input.GetKey(InputManager.instance.GrappleBtn))
            {
                if (!posIsUpdated)
                {
                    rotatePelletParent.position = GameManager.instance.player.transform.position;
                    startRot = rotatePelletParent.eulerAngles;
                    if (bossCart.GetComponent<SC_Movement>().playerOnLeftSide)
                    {
                        isLeftSide = true;
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotatePelletParent.eulerAngles.y - 90, transform.eulerAngles.z);
                    }
                    else if (!bossCart.GetComponent<SC_Movement>().playerOnLeftSide)
                    {
                        isLeftSide = false;
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotatePelletParent.eulerAngles.y + 90, transform.eulerAngles.z);
                    }
                    posIsUpdated = true;
                    canHurtBoss = true;
                }

                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().useGravity = false;

                transform.parent = rotatePelletParent;
                rotateNow = true;
            }
        }
        

    }
    private void OnMouseExit()
    {
        grappleUI.enabled = false;
    }
    private void Update()
    {
        if (rotateNow)
        {

            if (isLeftSide)
            {
                rotatePelletParent.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
                if (rotatePelletParent.eulerAngles.y <= startRot.y + 60)
                {

                    ShootBack();
                    rotateNow = false;
                }
            }
            else if (!isLeftSide)
            {
                rotatePelletParent.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                if (rotatePelletParent.eulerAngles.y >= startRot.y + 300)
                {

                    ShootBack();
                    rotateNow = false;
                }
            }

        }
    }
    public void ShootBack()
    {
        transform.parent = null;
        rotatePelletParent.eulerAngles = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        transform.LookAt(bossCart.transform.position);
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 20f, ForceMode.Impulse);
    }
}
