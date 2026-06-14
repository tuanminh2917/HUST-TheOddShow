using UnityEngine;

public class Printer : MonoBehaviour
{
    public bool isPrinted = false;
    public bool isDisplay = false;
    public GameObject key;

    private void Start()
    {
        key.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPrinted && !isDisplay)
        {
            key.SetActive(true);
            isDisplay = true;
        }
    }
}
