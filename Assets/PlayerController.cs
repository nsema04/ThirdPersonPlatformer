using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public float gravity = -9.8f;

    private CharacterController controller;
    private Vector3 velocity;
    private int jumpCount = 0; 

    public Transform cameraTransform;

    // Dash variables
    public float dashSpeedMultiplier = 2f; // 2x speed when dashing
    public float dashDuration = 0.2f; // Dash lasts 0.2 seconds
    public float dashCooldown = 1f; // 1-second cooldown between dashes
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        // Check if the player is on the ground
        bool isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            velocity.y = -2f; // Reset velocity when grounded
            jumpCount = 0; // Reset jump count when touching the ground
        }

        // Handle dashing cooldown
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        // Camera movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        Vector3 move = (camForward * vertical + camRight * horizontal).normalized;
        float currentSpeed = speed;

        // Dash logic
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        // If dashing, increase speed temporarily
        if (isDashing)
        {
            currentSpeed *= dashSpeedMultiplier;
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                isDashing = false;
            }
        }

        // Rotate chicken towards movement direction
        if (move.magnitude > 0.1f) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jumping logic with double jump
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpCount++; // Increment jump count
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
