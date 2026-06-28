using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    [Header("Button")]
    public Button privateFolderBtn;
    public Button print_fileFolderBtn;
    public Button logsFolderBtn;

    [Header("Corresponding Windows")]
    public GameObject privateWindow;
    public GameObject print_fileWindow;
    public GameObject logsWindow;
    void Start()
    {
        privateFolderBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Click");
            privateWindow.SetActive(true);
        });

        print_fileFolderBtn.onClick.AddListener(() => {
            AudioManager.Instance.Play("Click");
            print_fileWindow.SetActive(true);
        });

        logsFolderBtn.onClick.AddListener(() => {
            AudioManager.Instance.Play("Click");
            logsWindow.SetActive(true);
        });
    }
}
