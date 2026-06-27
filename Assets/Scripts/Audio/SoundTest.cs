using UnityEngine;
using UnityEngine.UI;

public class SoundTest : MonoBehaviour
{
    [SerializeField] private Button clickMeBtn;
    [SerializeField] private Button bgmBtn;

    private void Start()
    {
        clickMeBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Click");
        });

        bgmBtn.onClick.AddListener(() => {
            AudioManager.Instance.Play("BGM1", 7.0f);
        });
    }
}
