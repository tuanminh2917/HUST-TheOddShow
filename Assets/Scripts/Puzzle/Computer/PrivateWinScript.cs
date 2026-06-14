using UnityEngine;
using UnityEngine.UI;

public class PrivateWinScript : MonoBehaviour
{
    [Header("Backward Button")]
    public Button backwardBtn;
    void Start()
    {
        backwardBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
