using UnityEngine;
using UnityEngine.UI;

public class ComputerController : MonoBehaviour
{
    public GameObject passwordScreen;
    public GameObject openScreen;
    public GameObject keyModelScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        passwordScreen.SetActive(true);
        passwordScreen.GetComponent<PasswordScreen>().functionalButtons[1].onClick.AddListener(() =>
        {
            openScreen.SetActive(true);
        });

        openScreen.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            openScreen.SetActive(false);
            keyModelScreen.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
