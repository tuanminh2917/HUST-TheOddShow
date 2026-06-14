using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialType tutorialToTrigger;

    // Trường hợp 1: Kích hoạt khi người chơi đi vào vùng (Dùng Collider dạng IsTrigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialManager.Instance.TriggerTutorial(tutorialToTrigger);
        }
    }

    // Trường hợp 2: Kích hoạt dựa trên khoảng cách (Nếu không dùng Vật lý/Trigger)
    // Bạn có thể gọi hàm này từ hệ thống AI hoặc hệ thống tương tác của bạn
    public void CheckDistanceAndTrigger(Vector3 playerPosition)
    {
        float distance = Vector3.Distance(transform.position, playerPosition);
        if (distance < 3f) // Khoảng cách đủ gần
        {
            TutorialManager.Instance.TriggerTutorial(tutorialToTrigger);
        }
    }
}