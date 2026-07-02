using UnityEngine;
using UnityEngine.UI;

public class PrivateWinScript : MonoBehaviour
{
    [Header("Backward Button")]
    public Button backwardBtn;
    public Button formulaIcon;

    public GameObject formulaScreen;
    void Start()
    {
        backwardBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Click");
            gameObject.SetActive(false);
        });

        formulaIcon.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play("Click");
            formulaScreen.SetActive(true);
        });
    }
}
