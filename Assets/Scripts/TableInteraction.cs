using UnityEngine;

public class TableInteraction : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Drag the GameObject you want to appear on the right panel here.")]
    public GameObject rightPanelContent;

    void Start()
    {
        // Make sure the content is hidden when the game starts
        if (rightPanelContent != null)
        {
            rightPanelContent.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that bumped into the table is the Player
        if (other.CompareTag("Player"))
        {
            if (rightPanelContent != null)
            {
                rightPanelContent.SetActive(true); // Show the content
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the Player has walked away
        if (other.CompareTag("Player"))
        {
            if (rightPanelContent != null)
            {
                rightPanelContent.SetActive(false); // Hide the content
            }
        }
    }
}