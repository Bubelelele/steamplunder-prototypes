using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonPipe : MonoBehaviour
{
    [SerializeField] private GameObject steam;
    [SerializeField] private Animator _animator;
    [SerializeField] private float durationClosed = 0;

    private float timer = 0;
    private bool playerNearby = false;

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.F))
        {
            _animator.SetBool("Retract", true);
        }

        if (_animator.GetBool("Retract"))
        {
            timer += Time.deltaTime;
        }

        if (timer > durationClosed)
        {
            _animator.SetBool("Retract", false);
            timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerNearby = false;
        }
    }

    public void ToggleSteam()
    {
        steam.SetActive(!steam.activeSelf);
    }

    public void CountUp()
    {
        if (_animator.GetBool("Retract"))
        {
            PistonPuzzleManager.instance.counter++;
        }
    }

    public void ResetCounter()
    {
        if (!_animator.GetBool("Retract"))
        {
            PistonPuzzleManager.instance.counter--;
        }
    }
}
