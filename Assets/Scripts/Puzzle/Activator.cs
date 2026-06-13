using UnityEngine;

public class Activator : MonoBehaviour
{
    [Header("Kéo bảng Puzzle UI hoặc Object Puzzle vào đây")]
    public GameObject puzzle;

    private void Start()
    {
        // Mặc định khi vào game, ẩn puzzle này đi cho đến khi Player dẫm vào ô kích hoạt
        if (puzzle != null)
        {
            puzzle.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra an toàn: Nếu không tìm thấy va chạm hoặc quên chưa kéo thả puzzle vào Inspector -> Thoát luôn
        if (collision == null || puzzle == null) { return; }

        // SỬA: Chuyển dấu nháy đơn thành nháy kép "" cho Tag
        if (collision.CompareTag("Player"))
        {
            // Nếu tương tác với Player, hiển thị puzzle lên màn hình
            puzzle.SetActive(true);
            Debug.Log("Player đã đi vào vùng kích hoạt! Mở Puzzle.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Kiểm tra an toàn tương tự như trên
        if (collision == null || puzzle == null) { return; }

        if (collision.CompareTag("Player"))
        {
            // Khi Player đi ra khỏi vùng kích hoạt, tự động ẩn câu đố đi để tránh gian lận
            puzzle.SetActive(false);
            Debug.Log("Player đã rời khỏi vùng kích hoạt! Ẩn Puzzle.");
        }
    }
}