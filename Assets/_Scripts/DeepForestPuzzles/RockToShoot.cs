using UnityEngine;

public class RockToShoot : MonoBehaviour
{
    public GameObject Rock;
    public GameObject RockMesh;
    public GameObject player;
    public ParticleSystem rockDebrees;
    public ParticleSystem dustWave;
    public AudioSource destroyRockAudioSource;
    

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "EnergyBlast(Clone)")
        {
            Invoke("HideRock", 0.3f);
            Invoke("TurnOffRock", 1.3f);
        }
            }

    private void HideRock()
    {
        RockMesh.GetComponent<MeshRenderer>().enabled = false;
        rockDebrees.Play();
        dustWave.Play();
        destroyRockAudioSource.Play();
    }
    private void TurnOffRock()
    {
        Rock.SetActive(false);
    }
}
