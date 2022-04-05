using UnityEngine;

public class CylinderPuzzle : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private bool clockwise = true;
    [SerializeField] private float speed = 1f;
    public GameObject pressurePlate;

    private float _dir;

    private void Awake()
    {
        _dir = clockwise ? -1 : 1; 
    }

	private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotationSpeed * _dir, Space.Self);
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
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
    }

}
