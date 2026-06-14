using UnityEngine;
using UnityEngine.UI;

public class ComputerController : MonoBehaviour
{
    public GameObject passwordScreen;

    public bool solvable = false;
    void Start()
    {
        passwordScreen.SetActive(false);
    }

    private void Update()
    {
        if (solvable) {
            passwordScreen.SetActive(true);
        }
    }
}
