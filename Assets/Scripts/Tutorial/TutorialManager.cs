using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Nếu bạn dùng TextMeshPro

public enum TutorialType
{
    ClickToMove,
    ClickableItem,
    DraggableItem,
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    private Dictionary<TutorialType, bool> isTutorialCompleted = new Dictionary<TutorialType, bool>();
    private Dictionary<TutorialType, string> tutorialMessages = new Dictionary<TutorialType, string>();

    // Lưu danh sách các hướng dẫn thuộc vùng/vật phẩm mà người chơi đang tiếp cận
    private List<TutorialType> currentZoneTutorials = new List<TutorialType>();

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); return; }

        tutorialMessages[TutorialType.ClickToMove] = "- Click vào bản đồ để di chuyển nhân vật";
        tutorialMessages[TutorialType.ClickableItem] = "- Một số vật phẩm có thể click";
        tutorialMessages[TutorialType.DraggableItem] = "- Một số vật phẩm có thể kéo";

        foreach (TutorialType type in System.Enum.GetValues(typeof(TutorialType)))
        {
            isTutorialCompleted[type] = false;
        }
    }

    private void Start()
    {
        // Đầu game chỉ hiện di chuyển
        UpdateTutorialZone(new List<TutorialType> { TutorialType.ClickToMove }, true);
    }

    // HÀM MỚI: Cập nhật danh sách hướng dẫn khi đi vào/ra khỏi vùng của vật phẩm
    public void UpdateTutorialZone(List<TutorialType> zoneTutorials, bool isEntering)
    {
        if (isEntering)
        {
            currentZoneTutorials = zoneTutorials;
            RefreshUI();
        }
        else
        {
            currentZoneTutorials.Clear();
            tutorialPanel.SetActive(false);
        }
    }

    // HÀM MỚI: Tự động quét và hiển thị những nội dung CHƯA hoàn thành
    public void RefreshUI()
    {
        string combinedMessage = "";
        bool hasAnyVisibleContent = false;

        foreach (TutorialType type in currentZoneTutorials)
        {
            // Nếu hướng dẫn này chưa làm xong thì mới thêm vào dòng chữ hiển thị
            if (!isTutorialCompleted[type])
            {
                if (combinedMessage != "") combinedMessage += "\n"; // Xuống dòng nếu có nhiều hơn 1 thông báo
                combinedMessage += tutorialMessages[type];
                hasAnyVisibleContent = true;
            }
        }

        if (hasAnyVisibleContent)
        {
            tutorialText.text = combinedMessage;
            tutorialPanel.SetActive(true);
        }
        else
        {
            tutorialPanel.SetActive(false); // Nếu hoàn thành hết rồi thì ẩn Panel đi
        }
    }

    // HÀM HOÀN THÀNH: Gọi khi người chơi thực hiện thao tác thành công
    public void CompleteTutorial(TutorialType type)
    {
        if (isTutorialCompleted.ContainsKey(type))
        {
            isTutorialCompleted[type] = true;
        }

        // Cập nhật lại UI ngay lập tức để xóa dòng chữ vừa làm xong
        RefreshUI();
    }
}