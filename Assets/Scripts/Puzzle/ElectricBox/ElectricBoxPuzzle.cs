using UnityEngine;

public class ElectricBoxPuzzle : MonoBehaviour
{
    public UnpowerWireStats1[] unPoweredWireS;
    public GameObject electricBoxPuzzle;

    // Update is called once per frame
    void Update()
    {
        foreach (var unpower in unPoweredWireS)
        {
            if (unpower.connected == false) return;
        }
        electricBoxPuzzle.SetActive(true);
        electricBoxPuzzle.GetComponentInChildren<ElectricBox>().isConnected = true;
        gameObject.SetActive(false);
    }
}
