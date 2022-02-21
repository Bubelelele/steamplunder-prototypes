using UnityEngine;

public class PistonSpark : MonoBehaviour
{
    public ParticleSystem sparks;
    public void Sparks()
    {
        sparks.Play();
    }
}
