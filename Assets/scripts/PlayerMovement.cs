using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class PlayerMovement : MonoBehaviour
{
    public float speed = 4f;
    public float sprintSpeedMultiplier = 6f; // Reduced speed multiplier when sprinting
    public float rotationSpeed;
    public float jumpSpeed;
    private CharacterController controller;
    private Animator animator;
    private float ySpeed;
    private float originalStepOffset;
    private bool isSprinting = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        originalStepOffset = controller.stepOffset;
    }

    void Update()
{
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");
    bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift); // Check if Shift key is pressed

    if (verticalInput < 0)
    {
        verticalInput = 0;
    }

    Vector3 forwardDirection = transform.forward;

    Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
    Vector3 movementDirection = transform.TransformDirection(inputDirection).normalized;

    float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;

    // Adjust speed for sprinting
    if (isSprinting)
    {
        magnitude *= 1.5f; // Increase speed when sprinting
    }

    ySpeed += Physics.gravity.y * Time.deltaTime;

    if (controller.isGrounded)
    {
        controller.stepOffset = originalStepOffset;
        ySpeed = -0.5f;

        if (Input.GetButtonDown("Jump"))
        {
            ySpeed = jumpSpeed;
            animator.SetTrigger("JumpTrigger");
        }
    }
    else
    {
        controller.stepOffset = 0;
    }

    Vector3 velocity = movementDirection * magnitude;
    velocity.y = ySpeed;

    controller.Move(velocity * Time.deltaTime);

    // Rotate the player in place
    if (horizontalInput != 0 || verticalInput != 0)
    {
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    animator.SetFloat("Speed", magnitude);
}

}

