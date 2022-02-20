using UnityEngine;

public class BarricadeTrigger : MonoBehaviour
{
    public GameObject[] barricade;
    public GameObject invisibleBarricade;
    public GameObject wholePrefab;
    public ParticleSystem splinter;

    private int hitCounter = 0;
    private bool localCanAttack = false;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            if (Input.GetKey(KeyCode.Mouse0) && localCanAttack)
            {
                localCanAttack = false;

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
    private void Update()
    {
        if (GameManager.instance.player.gameObject.GetComponent<AxeController>().CanAttack)
        {
            localCanAttack = true;
        }
    }
    private void HideObject()
    {
        wholePrefab.gameObject.SetActive(false);
    }
}
