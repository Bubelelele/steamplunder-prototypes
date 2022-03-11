using UnityEngine;

public class HammerSparks : MonoBehaviour
{
    public ParticleSystem leftSparks;
    public ParticleSystem rightSparks;
    public void LeftSparks()
    {
        leftSparks.Play();
    }
    public void RightSparks()
    {
        rightSparks.Play();
    }
}
