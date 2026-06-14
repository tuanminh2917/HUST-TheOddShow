using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Nếu bạn dùng TextMeshPro

public enum TutorialType
{
    ClickToMove,
    ClickableItem,
    DraggableItem,
    Clickable_DragableItem
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [System.Serializable]
    public struct TutorialContent
    {
        public TutorialType type;
        [TextArea] public string message;
    }

    [Header("Cấu hình nội dung")]
    public List<TutorialContent> tutorialList;

    [Header("Giao diện UI")]
    public GameObject tutorialPanel; // Panel chứa Text
    public TextMeshProUGUI tutorialText; // Thành phần hiển thị chữ
    public float displayDuration = 4f; // Thời gian tự ẩn hướng dẫn

    // Dictionary để lưu trạng thái: Loại hướng dẫn -> Đã hiển thị chưa?
    private Dictionary<TutorialType, bool> tutorialStatus = new Dictionary<TutorialType, bool>();
    private Dictionary<TutorialType, string> tutorialMessages = new Dictionary<TutorialType, string>();
    private Coroutine hideCoroutine;

    private void Awake()
    {
        // Khởi tạo Singleton
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); return; }

        // Khởi tạo dữ liệu
        foreach (var item in tutorialList)
        {
            tutorialStatus[item.type] = false; // Ban đầu tất cả đều chưa hiển thị
            tutorialMessages[item.type] = item.message;
        }
    }

    private void Start()
    {
        // Trò chơi bắt đầu: Hiển thị ngay hướng dẫn Di chuyển
        TriggerTutorial(TutorialType.ClickToMove);
    }

    // Hàm public để các đối tượng khác gọi khi thỏa mãn điều kiện
    public void TriggerTutorial(TutorialType type)
    {
        // KIỂM TRA: Nếu đã hiển thị rồi thì bỏ qua không làm gì cả
        if (tutorialStatus.ContainsKey(type) && tutorialStatus[type] == true)
        {
            return;
        }

        // Nếu chưa hiển thị, tiến hành hiển thị
        ShowTutorialUI(type);
    }

    private void ShowTutorialUI(TutorialType type)
    {
        // Đánh dấu là đã hiển thị (Đảm bảo chỉ xuất hiện 1 lần duy nhất)
        tutorialStatus[type] = true;

        // Hiển thị Text lên UI
        tutorialText.text = tutorialMessages[type];
        tutorialPanel.SetActive(true);

        // Xử lý đếm ngược để ẩn UI
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(Co_HideTutorial());
    }

    private IEnumerator Co_HideTutorial()
    {
        yield return new WaitForSeconds(displayDuration);
        tutorialPanel.SetActive(false);
    }
}