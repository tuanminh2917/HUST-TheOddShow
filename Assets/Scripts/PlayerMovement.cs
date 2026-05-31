using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    [Tooltip("How close the player needs to get to the click before stopping.")]
    public float stopDistance = 0.05f;

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Start the target position at the player's current spot so they don't move immediately
        targetPosition = rb.position;
    }

    void Update()
    {
        // Check for left mouse button click (0 is left click, 1 is right click)
        if (Input.GetMouseButtonDown(0))
        {
            // Convert the screen pixel coordinate of the mouse to a world coordinate
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Set the target position, ignoring the Z axis (since it's a 2D game)
            targetPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            isMoving = true;
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            // Check if we are far enough away from the target to keep moving
            if (Vector2.Distance(rb.position, targetPosition) > stopDistance)
            {
                // Calculate the next step towards the target
                Vector2 nextPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

                // Move the Rigidbody to the new step
                rb.MovePosition(nextPosition);
            }
            else
            {
                // We arrived at the destination!
                isMoving = false;
            }
        }
    }
}