using UnityEngine;

public class ElectricBoxPuzzle : MonoBehaviour
{
    public UnpowerWireStats[] unPoweredWireS;
    public GameObject electricBox;

    // Update is called once per frame
    void Update()
    {
        foreach (var unpower in unPoweredWireS)
        {
            if (unpower.connected == false) return;
        }
        electricBox.SetActive(true);
        electricBox.GetComponentInChildren<ElectricBox>().isConnected = true;
        gameObject.SetActive(false);
    }
}
