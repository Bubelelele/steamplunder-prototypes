using UnityEngine;

public class BarricadeTrigger : MonoBehaviour
{
    public GameObject[] barricade;
    public GameObject invisibleBarricade;
    public GameObject wholePrefab;
    public ParticleSystem splinter;

    private int hitCounter = 0;
    private bool hasEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            hasEntered = true;                        
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            hasEntered = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && GameManager.instance.player.gameObject.GetComponent<AxeController>().CanAttack && hasEntered)
        {
            {
                
                if (hitCounter == 0)
                {
                    splinter.transform.position = barricade[0].transform.position;
                    splinter.Play();
                    Destroy(barricade[0]);
                    hitCounter++;
                }
                else if (hitCounter == 1)
                {
                    splinter.transform.position = barricade[1].transform.position;
                    splinter.Play();
                    Destroy(barricade[1]);
                    hitCounter++;
                }
                else if (hitCounter == 2)
                {
                    splinter.transform.position = barricade[2].transform.position;
                    splinter.Play();
                    Destroy(barricade[2]);
                    Invoke("HideObject", 0.5f);
                }
            }
        }
    }
    private void HideObject()
    {
        wholePrefab.gameObject.SetActive(false);
    }
}
