using UnityEngine;
using System.Collections;

public class CylinderPuzzle : CylinderPressurePlates
{

    [SerializeField] private float rotationSpeed = 1.5f;
    [SerializeField] private bool clockwise = true;
    [SerializeField] private float speed = 0f;
    private float _dir;
    
    

    public Vector3 _respawnPos;
   

    private void Awake()
    {
        _dir = clockwise ? -1 : 1;
        
        _respawnPos = GameObject.Find("Respawn").transform.position;
        
    }

	private void FixedUpdate()
    {
  
            StartCoroutine(PuzzleStart());
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("MoveBox"))
		{ 
            speed = -speed;
            _dir = -_dir;
        }
		if (other.gameObject.CompareTag("CorrectBox"))
		{
            transform.Rotate(Vector3.up, rotationSpeed * _dir, Space.Self);
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }
		if (other.gameObject.CompareTag("PuzzleSolved"))
		{
            speed = 0;
            _dir = 0;
		}
        if (other.gameObject.CompareTag("Player"))
		{
            Bean.transform.position =_respawnPos;
		}
	
       
    }

    IEnumerator PuzzleStart()
	{
        transform.Rotate(Vector3.up, rotationSpeed * _dir, Space.Self);
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        yield return new WaitForSeconds(0);
    }
}
