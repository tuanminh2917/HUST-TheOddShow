using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    // Thay đổi từ một biến đơn lẻ thành một List
    public List<TutorialType> tutorialsToTrigger = new List<TutorialType>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Truyền cả danh sách vào Manager
            TutorialManager.Instance.UpdateTutorialZone(tutorialsToTrigger, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialManager.Instance.UpdateTutorialZone(tutorialsToTrigger, false);
        }
    }
}