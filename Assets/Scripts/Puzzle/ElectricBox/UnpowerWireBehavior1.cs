using Unity.VisualScripting;
using UnityEngine;

public class UnpowerWireBehavior1 : MonoBehaviour
{
    UnpowerWireStats1 unpowerdWireS;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unpowerdWireS = gameObject.GetComponent<UnpowerWireStats1>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageLight();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PoweredWireStats1>() != null) { 
            PoweredWireStats1 poweredWireS = collision.GetComponent<PoweredWireStats1>();
            if (poweredWireS.objectColor == unpowerdWireS.objectColor) {
                poweredWireS.connected = true;
                unpowerdWireS.connected = true;
                poweredWireS.connectedPosition = gameObject.transform.position;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PoweredWireStats1>() != null)
        {
            PoweredWireStats1 poweredWireS = collision.GetComponent<PoweredWireStats1>();
            poweredWireS.connected = false;
            unpowerdWireS.connected = false;

        }
    }

    void ManageLight()
    {
        if (unpowerdWireS.connected)
        {
            unpowerdWireS.poweredLight.SetActive(true);
            unpowerdWireS.unpoweredLight.SetActive(false);
        }
        else
        {
            unpowerdWireS.poweredLight.SetActive(false);
            unpowerdWireS.unpoweredLight.SetActive(true);
        }
    }
}
