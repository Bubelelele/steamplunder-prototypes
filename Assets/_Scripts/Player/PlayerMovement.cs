using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private bool lookAtMouse;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask mouseAreaLayer;
    
    [Header("References")] 
    [SerializeField] private Camera cam;
    
    private InputManager _input;
    
    private void Start() {
        _input = InputManager.instance;
        GameManager.instance.player = transform;
    }

    private void FixedUpdate() {
        if (GameManager.instance.state != GameState.Default) return;
        
        Vector3 inputVector = _input.InputDir3;
        Vector3 movementVector = GetMovementVector(inputVector);
        movementVector = AdjustVelocityToSlope(movementVector);
        
        Move(movementVector);
        
        if (lookAtMouse)
            RotateToMouse();
        else
            RotateToMovement(movementVector);
    }

    //Rotate to always look at the mouse
    private void RotateToMouse() {
        Ray ray = cam.ScreenPointToRay(_input.MousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: 300f, mouseAreaLayer)) {
            Vector3 target = hit.point;
            target.y = transform.position.y;
            
            transform.LookAt(target);
        }
    }

    //Rotate to the direction of the player's movement
    private void RotateToMovement(Vector3 movementVector) {
        if (movementVector.magnitude == 0) return;
        
        Quaternion rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed);
    }

    //Move the the player transform by movementVector
    private void Move(Vector3 movementVector) {
        float speed = moveSpeed * Time.deltaTime;
        Vector3 targetPosition = transform.position + movementVector * speed;
        
        transform.position = targetPosition;
    }

    //Get our movementVector by offsetting our inputVector by the camera's y rotation
    private Vector3 GetMovementVector(Vector3 inputVector) {
        Vector3 movementVector = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * inputVector;
        return movementVector;
    }

    //Fixes bounces while walking down a slope
    private Vector3 AdjustVelocityToSlope(Vector3 velocity) {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 1.1f, groundLayer)) {
            Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Vector3 adjustedVelocity = slopeRotation * velocity;

            if (adjustedVelocity.y < 0) return adjustedVelocity;
        }

        return velocity;
    }
}