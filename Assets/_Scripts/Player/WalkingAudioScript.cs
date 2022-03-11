using UnityEngine;

public class WalkingAudioScript : MonoBehaviour
{
    private bool audioPlaying = false;
    private void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (!audioPlaying)
            {
                AudioManager.instance.Play("walking");
                audioPlaying = true;
            }
        }
        else
        {
            if (audioPlaying)
            {
                AudioManager.instance.Stop("walking");
                audioPlaying = false;
            }
        }
    }
}
