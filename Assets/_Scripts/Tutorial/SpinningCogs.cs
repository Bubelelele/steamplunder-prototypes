using UnityEngine;

public class SpinningCogs : MonoBehaviour
{
    public bool Spin = true;

    void Update()
    {
        if (Spin)
        {
            transform.Rotate(new Vector3(0f, 100f * Time.deltaTime, 0f), Space.Self);
        }
    }
}
