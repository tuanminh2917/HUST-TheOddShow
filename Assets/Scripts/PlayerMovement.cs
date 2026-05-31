using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        // Automatically grab the Rigidbody2D component attached to the Player
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Gather input from the player (WASD or Arrow Keys)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 2. Normalize the vector so diagonal movement isn't faster
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // 3. Apply the movement to the Rigidbody in FixedUpdate for smooth physics
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}