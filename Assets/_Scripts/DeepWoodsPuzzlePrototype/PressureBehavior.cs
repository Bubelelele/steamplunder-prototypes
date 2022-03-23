using UnityEngine;
using System.Collections;

public class PressureBehavior : MonoBehaviour
{
    
    public GameObject PressurePlate0;
    public GameObject PressurePlate1;
    public GameObject PressurePlate2;
    public GameObject PressurePlate3;
    public GameObject PressurePlate4;

    public GameObject Cog0;
    public GameObject cog1;

    public GameObject Bridge;

    public GameObject Steam;
    public GameObject[] Steam2 = new GameObject[2];
    public GameObject[] Steam3 = new GameObject[3];
    public GameObject[] Steam4 = new GameObject[4];
    public GameObject[] Steam5 = new GameObject[5];

    bool[] plate = { false, false, false, false, false };

    private bool isCoroutineExecuting = false;
  

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(plate[0] + "0");
        Debug.Log(plate[1] + "1");
        Debug.Log(plate[2] + "2")  ;
        Debug.Log(plate[3] + "3");
        Debug.Log(plate[4] + "4");

        
    }

	private void OnTriggerEnter(Collider other)
	{
        
        if (other.gameObject == PressurePlate0)
        {
            if (!(plate[0] && plate[1] && plate[2] && plate[3] && plate[4]))
            {
               ParticleSystem SteamLooping = Steam.GetComponent<ParticleSystem>();
                SteamLooping.loop = false;

                plate[0] = true;
            }
        }
        if (other.gameObject == PressurePlate1)
        {
            if (plate[0] == true)
            {
				for (var i = 0; i < Steam2.Length; i++)
				{
                    ParticleSystem SteamLooping1 = Steam2[i].GetComponent<ParticleSystem>();
                    SteamLooping1.loop = false;
                }

                plate[1] = true;
            }
        }
		if (other.gameObject == PressurePlate2)
		{
			if (plate[1] == true)
			{
                for (var i = 0; i < Steam3.Length; i++)
                {
                    ParticleSystem SteamLooping2 = Steam3[i].GetComponent<ParticleSystem>();
                    SteamLooping2.loop = false;
                }

                plate[2] = true;
            }
		}
		if (other.gameObject == PressurePlate3)
		{
			if (plate[2] == true)
			{
                for (var i = 0; i < Steam4.Length; i++)
                {
                    ParticleSystem SteamLooping3 = Steam4[i].GetComponent<ParticleSystem>();
                    SteamLooping3.loop = false;
                }

                plate[3] = true;
            }
		}
		if (other.gameObject == PressurePlate4)
		{
			if (plate[3] == true)
			{

                for (var i = 0; i < Steam5.Length; i++)
                {
                    ParticleSystem SteamLooping5 = Steam5[i].GetComponent<ParticleSystem>();
                    SteamLooping5.loop = false;
                }

                plate[4] = true;
            }
        }

		if (plate[4] == true)
		{
            PuzzleClear();
            StartCoroutine(Waiting());
        }
    }
    private void PuzzleClear()
	{
        Cog0.GetComponent<Spin>().enabled = true;
        
        cog1.GetComponent<Spin>().enabled = true;
        

        Rigidbody BridgeRigidbody = Bridge.GetComponent<Rigidbody>();
        BridgeRigidbody.isKinematic = false;

        

    }
    IEnumerator Waiting()
	{
		if (isCoroutineExecuting == false)
		{

            isCoroutineExecuting = true;

            yield return new WaitForSeconds(3);

            Cog0.GetComponent<Spin>().enabled = false;

            cog1.GetComponent<Spin>().enabled = false;

            isCoroutineExecuting = false;

            yield break;
        }
      
    }
}
     
    


    


