using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterNavigation : MonoBehaviour
{
    public Transform characterTransform; // The player UI image
    public float walkSpeed = 300f;
    public Animator animator;      // UI pixels per second

    // References to your nodes (Assign these in the Inspector)
    public Transform nodeCenter;
    public Transform nodeDesk;
    public Transform nodeComputer;
    public Transform nodeSink;
    public Transform nodePrinter;
    public Transform nodeLocker;
    public Transform nodeElectric;
    public Transform nodeGirl;

    private Transform currentNode;

    void Start()
    {
        // Start the game at the center (or wherever your default is)
        currentNode = nodeCenter;
        characterTransform.position = currentNode.position;
        SetAnimatorDirection(0, -1);
    }

    // Call this from ALL your invisible click-zone buttons
    public void MoveToInteractable(Transform targetNode)
    {
        if (currentNode == targetNode) return; // Already there!

        List<Transform> path = new List<Transform>();

        // --- SMART OBSTACLE CHECKING ---
        // We only need to use NodeCenter if we are crossing from the LEFT side 
        // of the giant desk to the RIGHT side (or vice versa).

        bool startingOnLeft = (currentNode == nodeComputer || currentNode == nodeSink || currentNode == nodePrinter);
        bool endingOnRight = (targetNode == nodeElectric || targetNode == nodeLocker || targetNode == nodeGirl);

        bool startingOnRight = (currentNode == nodeElectric || currentNode == nodeLocker || currentNode == nodeGirl);
        bool endingOnLeft = (targetNode == nodeComputer || targetNode == nodeSink || targetNode == nodePrinter);

        // Scenario 1: Crossing from Left to Right
        if (startingOnLeft && endingOnRight)
        {
            path.Add(nodeCenter); // Take the detour
        }
        // Scenario 2: Crossing from Right to Left
        else if (startingOnRight && endingOnLeft)
        {
            path.Add(nodeCenter); // Take the detour
        }
        // Scenario 3: Going to or from the Center Desk itself
        else if (currentNode == nodeDesk && (endingOnLeft || endingOnRight))
        {
            // The center desk is already near the middle, walk straight to the side nodes
        }

        // Always add the final destination at the end of the path
        path.Add(targetNode);

        // Execute the movement smoothly
        StopAllCoroutines();
        StartCoroutine(FollowPathRoutine(path, targetNode));
    }

    private IEnumerator FollowPathRoutine(List<Transform> path, Transform finalDestination)
    {
        if (animator != null) animator.SetBool("isWalking", true);
        foreach (Transform target in path)
        {
            Vector3 direction = (target.position - characterTransform.position).normalized;
            SetAnimatorDirection(direction.x, direction.y);
            while (Vector2.Distance(characterTransform.position, target.position) > 1f)
            {
                characterTransform.position = Vector3.MoveTowards(
                    characterTransform.position,
                    target.position,
                    walkSpeed * Time.deltaTime
                );
                yield return null;
            }
            currentNode = target;
        }
        if (animator != null) animator.SetBool("isWalking", false);
        SetAnimatorDirection(0, 0);
        Debug.Log("Arrived at: " + finalDestination.name);
    }

    private void SetAnimatorDirection(float x, float y)
    {
        if (animator == null) return;

        float snapX = 0f;
        float snapY = 0f;

        // Check if the character is moving more horizontally than vertically
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            // Snap to pure Left (-1) or pure Right (1)
            snapX = Mathf.Sign(x);
            snapY = 0f;
        }
        else if (Mathf.Abs(y) > Mathf.Abs(x))
        {
            // Snap to pure Down (-1) or pure Up / Back (1)
            snapX = 0f;
            snapY = Mathf.Sign(y);
        }
        else if (Mathf.Abs(x) > 0.1f)
        {
            // If they are EXACTLY equal (like 1,1), pick one as a default (e.g., horizontal)
            snapX = Mathf.Sign(x);
            snapY = 0f;
        }

        // If the character completely stopped, pass (0,0) so the Blend Tree handles idle
        if (x == 0f && y == 0f)
        {
            snapX = 0f;
            snapY = 0f;
        }

        // Send the perfectly snapped numbers to your Blend Tree!
        animator.SetFloat("moveX", snapX);
        animator.SetFloat("moveY", snapY);
    }
}