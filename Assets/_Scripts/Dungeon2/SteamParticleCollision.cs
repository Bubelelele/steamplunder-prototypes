using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamParticleCollision : MonoBehaviour
{
    public GameObject player;

    private PlayerStats stats;

    private void Start()
    {
        stats = player.GetComponent<PlayerStats>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            stats.Damage(1);
        }
    }
}
