using UnityEngine;

public class PuzzleLogic : MonoBehaviour
{
    public Transform[] platforms;
    public PlateLogic[] plates;

    void Update()
    {
        if (plates[0].isPressed)
        {
            LiftPlatform(platforms[0]);

            if (plates[1].isPressed)
            {
                LiftPlatform(platforms[1]);

                if (plates[2].isPressed)
                {
                    LiftPlatform(platforms[2]);
                }
            }
            else if (plates[2].isPressed)
            {
                Reset();
            }
        }
        else if (plates[1].isPressed || plates[2].isPressed)
        {
            Reset();
        }
    }

    void LiftPlatform(Transform platform)
    {
        Debug.Log("ehy");
        if (platform.localPosition.y < 0)
        {
            platform.Translate(Vector3.up * 0.36f * Time.deltaTime, Space.Self);
        }
    }

    void Reset()
    {
        foreach (Transform platform in platforms)
        {
            if (platform.localPosition.y > -0.36f)
            {
                platform.Translate(Vector3.down * 0.36f, Space.Self);
            }
        }

        foreach (PlateLogic plate in plates)
        {
            plate.isPressed = false;
        }
    }
}
