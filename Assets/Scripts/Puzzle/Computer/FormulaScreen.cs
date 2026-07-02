using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormulaScreen : MonoBehaviour
{
    [Header("Buttons")]
    public Button[] btnList;
    public Button backwardBtn;

    [Header("TMP_Text")]
    public TMP_Text tmp_Text;

    [Header("Message")]
    [TextArea(3, 5)]
    public string[] message;

    [Header("Page index")]
    private int pageIdx = 0;

    private void Start()
    {
        ChangePage(0);

        btnList[0].onClick.AddListener(() => {
            AudioManager.Instance.Play("Click");
            if (pageIdx != 0)
            {
                pageIdx -= 1;
                ChangePage(pageIdx);
            }
        });

        btnList[1].onClick.AddListener(() => {
            AudioManager.Instance.Play("Click");
            if (pageIdx != message.Length - 1)
            {
                pageIdx += 1;
                ChangePage(pageIdx);
            }
        });

        backwardBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Click");
            gameObject.SetActive(false);
        });
    }

    private void ChangePage(int pageIdx)
    {
        tmp_Text.text = message[pageIdx];
    }
}
