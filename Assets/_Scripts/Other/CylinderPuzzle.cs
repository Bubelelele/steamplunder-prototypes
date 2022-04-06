using UnityEngine;
using System.Collections;

public class CylinderPuzzle : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private bool clockwise = true;
    [SerializeField] private float speed = 0f;
    private float _dir;

    private void Awake()
    {
        _dir = clockwise ? -1 : 1;
        speed = 2;
    }

	private void FixedUpdate()
    {
		if (Input.GetButton("Fire1"))
		{
            
            StartCoroutine(PuzzleStart());
        }
		if (Input.GetButtonUp("Fire1"))
		{
            speed = 2;
            _dir = clockwise ? -1 : 1;
        }
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("MovingBox"))
		{
            speed = -speed;
            _dir = -_dir;
        }
		if (other.gameObject.CompareTag("CorrectBox"))
		{
            Debug.Log("goodjob");
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
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        yield return new WaitForSeconds(0);
    }
}
