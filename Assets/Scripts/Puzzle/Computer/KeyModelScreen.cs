using UnityEngine;
using UnityEngine.UI;

public class KeyModelScreen : MonoBehaviour
{
    public Button backwardBtn;
    public Button printBtn;

    public GameObject printerPuzzle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        printBtn.onClick.AddListener(() => {
            AudioManager.Instance.Play("printer");
            printerPuzzle.SetActive(true);
            printerPuzzle.GetComponentInChildren<Printer>().isPrinted = true;
        });
        backwardBtn.onClick.AddListener(() => {
            AudioManager.Instance.Play("Click");
            gameObject.SetActive(false); 
        });
    }

}
