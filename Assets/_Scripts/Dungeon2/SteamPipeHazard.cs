using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPipeHazard : MonoBehaviour
{
    public ParticleSystem steam;
    public float interval;

    private void Start()
    {
        InvokeRepeating("AlternateSteam", 0f, interval);
    }

    private void AlternateSteam()
    {
        steam.Play();
    }
}
