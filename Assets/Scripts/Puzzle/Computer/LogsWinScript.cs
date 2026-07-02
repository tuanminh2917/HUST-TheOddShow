using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogsWinScript : MonoBehaviour
{
    [Header("Backward Button")]
    public Button backwardBtn;

    [Header("Logs Button")]
    public Button[] logsBtnList;

    
    [Header("Message")]
    [TextArea(3, 10)]
    public string[] message;
            

    [Header("Logs Screen")]
    public GameObject logsScreen;


    void Start()
    {
        backwardBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Click");
            gameObject.SetActive(false);
        });

        for (int i = 0; i < logsBtnList.Length; i++)
        {
            // SỬA Ở ĐÂY: Tạo một biến tạm để lưu giá trị i tại vị trí này
            int index = i;

            logsBtnList[index].onClick.AddListener(() =>
            {
                AudioManager.Instance.Play("Click");
                logsScreen.SetActive(true);

                // Lấy Component TMP_Text
                TMP_Text msgComponent = logsScreen.GetComponent<LogsScreen>().msgComponent;

                if (msgComponent != null)
                {
                    // Sử dụng biến index thay vì dùng i
                    string v = message[index];
                    msgComponent.text = v;

                    // --- ĐOẠN BỔ SUNG ĐỂ TỰ ĐỘNG CẬP NHẬT ĐỘ DÀI CONTENT ---
                    // Lấy RectTransform của Content (chính là cha của Text component)
                    RectTransform contentRect = msgComponent.transform.parent as RectTransform;
                    if (contentRect != null)
                    {
                        // Ép hệ thống giao diện UI cập nhật lại kích thước ngay lập tức
                        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
                    }
                }
            });
        }
    }
}
