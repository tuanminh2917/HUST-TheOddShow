using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Button continueBtn;
    public Button quitBtn;

    public GameObject introUI;
    public GameObject startUI;

    void Start()
    {
        introUI.SetActive(false);
        continueBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Click");
            startUI.gameObject.SetActive(false);
            introUI.SetActive(true);
            introUI.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                AudioManager.Instance.Play("Click");
                Debug.Log("Bắt đầu trò chơi");
                introUI.SetActive(false);
                SceneLoader.Instance.LoadNextScene("MidScene");
            });
        });
        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
