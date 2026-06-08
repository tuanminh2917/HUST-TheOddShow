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
        passwordScreen.SetActive(false);

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
