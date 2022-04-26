using UnityEngine;
using System.Collections;

public class CylinderPuzzle : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 1.5f;
    [SerializeField] private bool clockwise = true;
    [SerializeField] private float speed = 0f;
    private float _dir;
   

    public GameObject Bean;

    private bool CanCollide = true;

    private void Awake()
    {
        _dir = clockwise ? -1 : 1;
        
    }

	private void FixedUpdate()
    {
  
            StartCoroutine(PuzzleStart());
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("MoveBox") && CanCollide)
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
       
    }

    IEnumerator PuzzleStart()
	{
        transform.Rotate(Vector3.up, rotationSpeed * _dir, Space.Self);
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        yield return new WaitForSeconds(0);
    }
}
