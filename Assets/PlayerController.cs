using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 6f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
        ApplyGravity();
    }

    void MovePlayer()
    {
        // Check if the player is on the ground
        isGrounded = controller.isGrounded;

        // Get input from keyboard (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");    // W/S or Up/Down

        // Movement vector (relative to the world)
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        // Apply movement
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        // Apply gravity if not grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Keep grounded
        }

        // Jumping (if spacebar is pressed and player is grounded)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = jumpForce;
        }

        // Apply gravity force
        velocity.y += gravity * Time.deltaTime;

        // Move the player with gravity
        controller.Move(velocity * Time.deltaTime);
    }
}
