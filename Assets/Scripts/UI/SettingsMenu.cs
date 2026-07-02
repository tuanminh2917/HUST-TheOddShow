using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button mainmenuBtn;

    private void OnEnable()
    {
        // Khi bảng cài đặt được bật lên, gán giá trị từ AudioManager vào thanh UI
        if (AudioManager.Instance != null)
        {
            //if (bgmSlider != null) bgmSlider.value = AudioManager.Instance.GetBGMVolumeModifier();
            //if (sfxSlider != null) sfxSlider.value = AudioManager.Instance.GetSFXVolumeModifier();
        }

        //Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        //Time.timeScale = 1f;
    }

    private void Start()
    {
        resumeBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Click");
            gameObject.SetActive(false);
        });

        mainmenuBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Click");
            AudioManager.Instance.StopBGM();
            SceneLoader.Instance.LoadNextScene("MainMenu");
        });
    }
}