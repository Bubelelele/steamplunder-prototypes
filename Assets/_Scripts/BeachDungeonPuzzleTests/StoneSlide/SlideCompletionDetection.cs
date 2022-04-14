using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCompletionDetection : MonoBehaviour
{

    public GameObject rock;

    public GameObject invisWall;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rock.activeSelf)
        {
            GetComponent<Renderer>().material.color = Color.green;
            invisWall.SetActive(false);
        }
    }
}
