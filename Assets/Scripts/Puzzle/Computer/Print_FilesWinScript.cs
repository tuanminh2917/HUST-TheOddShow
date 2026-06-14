using UnityEngine;
using UnityEngine.UI;

public class Print_FilesWinScript : MonoBehaviour
{
    [Header("Backward Button")]
    public Button backwardBtn;

    [Header("Các nút khác")]
    public Button keyBtn;

    [Header("Windows")]
    public GameObject keyModelScreen;
    void Start()
    {
        backwardBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        keyBtn.onClick.AddListener(() => { 
            keyModelScreen.SetActive(true);
        });
    }
}
