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
    public string[] message;
            

    [Header("Logs Screen")]
    public GameObject logsScreen;


    void Start()
    {
        backwardBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        for (int i = 0; i < logsBtnList.Length; i++)
        {
            // SỬA Ở ĐÂY: Tạo một biến tạm để lưu giá trị i tại vị trí này
            int index = i;

            logsBtnList[index].onClick.AddListener(() =>
            {
                logsScreen.SetActive(true);

                // Lấy Component TMP_Text
                TMP_Text msgComponent = logsScreen.GetComponent<LogsScreen>().msgComponent;

                if (msgComponent != null)
                {
                    // Sử dụng biến index thay vì dùng i
                    string v = message[index];
                    msgComponent.text = v;
                }
            });
        }
    }
}
