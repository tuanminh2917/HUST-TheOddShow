using UnityEngine;
using TMPro; // Sử dụng TextMeshPro cho chữ sắc nét hơn

public class HintManager : MonoBehaviour
{
    [Header("--- UI Elements ---")]
    [SerializeField] private GameObject hintPopupPanel; // Panel chứa popup
    [SerializeField] private TextMeshProUGUI hintText;   // Thành phần Text hiển thị nội dung

    [Header("--- Hint Data (9 Stages) ---")]
    [TextArea(3, 5)] // Tạo ô nhập text rộng rãi trong Inspector
    [SerializeField] private string[] stageHints = new string[9]; // Mảng chứa 9 gợi ý

    private int currentStage = 0; // Tiến trình hiện tại (từ 0 đến 8)

    void Start()
    {
        // Đảm bảo popup ẩn khi mới vào game
        if (hintPopupPanel != null)
            hintPopupPanel.SetActive(false);
    }

    // 1. Hàm mở Popup và Tạm dừng game
    public void OpenHint()
    {
        if (hintPopupPanel == null || hintText == null) return;

        // Cập nhật nội dung chữ dựa vào stage hiện tại
        if (currentStage >= 0 && currentStage < stageHints.Length)
        {
            hintText.text = stageHints[currentStage];
        }
        else
        {
            hintText.text = "Không có gợi ý nào cho giai đoạn này.";
        }

        hintPopupPanel.SetActive(true); // Hiện popup
        Time.timeScale = 0f;            // Tạm dừng toàn bộ game
    }

    // 2. Hàm đóng Popup và Tiếp tục game
    public void CloseHint()
    {
        if (hintPopupPanel == null) return;

        hintPopupPanel.SetActive(false); // Ẩn popup
        Time.timeScale = 1f;             // Game chạy lại bình thường
    }

    // 3. Hàm dùng để các script khác gọi sang khi người chơi qua giai đoạn mới
    public void UpdateProgress(int newStage)
    {
        // Đảm bảo stage nằm trong khoảng từ 0 đến 8
        if (newStage >= 0 && newStage < stageHints.Length)
        {
            currentStage = newStage;
            Debug.Log("Đã chuyển sang giai đoạn gợi ý: " + (currentStage + 1));
        }
    }
}