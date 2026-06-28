using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Notebook : MonoBehaviour
{
    [Header("Buttons")]
    public Button[] btnList;

    [Header("TMP_Text")]
    public TMP_Text tmp_Text;

    [Header("Message")]
    [TextArea(3, 5)]
    public string[] message;

    [Header ("Page index")]    
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
            if (pageIdx != message.Length - 1) { 
                pageIdx += 1;
                ChangePage(pageIdx);
            } 
        });
    }

    private void ChangePage(int pageIdx)
    {
        tmp_Text.text = message[pageIdx];
    }
}
