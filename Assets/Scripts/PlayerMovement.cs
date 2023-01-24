using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CameraController cameraController;
    public float moveSpeed = 6f;
    public float rotationSpeed = 500f;

    private CharacterController characterController;
    private Quaternion targetRotation;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
         float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveInput = new Vector3(horizontal, 0.0f, vertical).normalized;
        Vector3 moveDirection = Quaternion.Euler(0, cameraController.rotationY, 0) * moveInput;
        targetRotation = Quaternion.LookRotation(moveDirection);

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
