using UnityEngine;
using UnityEngine.UI;

public class KeyModelScreen : MonoBehaviour
{

    public Button printBtn;

    public GameObject popUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        popUp.SetActive(false);
        printBtn.onClick.AddListener(() => { popUp.SetActive(true); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
