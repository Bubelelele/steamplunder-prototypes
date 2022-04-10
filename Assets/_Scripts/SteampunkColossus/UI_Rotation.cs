using UnityEngine;

public class UI_Rotation : MonoBehaviour
{
    public GameObject camera;

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
