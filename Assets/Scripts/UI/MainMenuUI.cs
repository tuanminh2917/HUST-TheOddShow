using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Button playBtn;
    void Start()
    {
        playBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("InGame");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
