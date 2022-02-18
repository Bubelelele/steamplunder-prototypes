using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Transform follow;
    [SerializeField] private Vector3 offset;

    void Update() {
        transform.position = follow.position + offset;
    }
}
