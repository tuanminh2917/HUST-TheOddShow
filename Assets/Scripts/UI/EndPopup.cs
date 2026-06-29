using UnityEngine;
using UnityEngine.UI;

public class EndPopup : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => {
            AudioManager.Instance.StopBGM();
            AudioManager.Instance.Play("Click");
            SceneLoader.Instance.LoadNextScene("CreditScene");
        });
    }
}
