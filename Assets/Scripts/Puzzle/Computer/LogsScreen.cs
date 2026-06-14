using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogsScreen : MonoBehaviour
{
    public Button backwardBtn;

    public TMP_Text msgComponent;

    private void Start()
    {
        backwardBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
