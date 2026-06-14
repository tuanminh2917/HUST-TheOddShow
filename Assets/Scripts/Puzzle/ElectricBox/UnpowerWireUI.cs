using UnityEngine;
using UnityEngine.EventSystems;

public class UnpowerWireUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    UnpowerWireStats1 unpowerdWireS;
    private RectTransform rectTransform;

    void Start()
    {
        unpowerdWireS = gameObject.GetComponent<UnpowerWireStats1>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        ManageLight();
    }

    // Khi đầu dây đang drag được di chuột vào vùng của điểm đích này
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Kiểm tra xem có phải đang kéo một đối tượng UI hay không
        if (eventData.pointerDrag != null)
        {
            PoweredWireUI poweredWireUI = eventData.pointerDrag.GetComponent<PoweredWireUI>();

            if (poweredWireUI != null)
            {
                PoweredWireStats1 poweredWireS = poweredWireUI.powerWireS;

                // Kiểm tra trùng màu
                if (poweredWireS.objectColor == unpowerdWireS.objectColor)
                {
                    poweredWireS.connected = true;
                    unpowerdWireS.connected = true;

                    // Gán vị trí đích bằng vị trí UI (anchoredPosition) của điểm này
                    poweredWireS.connectedPosition = rectTransform.anchoredPosition;
                }
            }
        }
    }

    // Khi đầu dây bị kéo ra khỏi vùng của điểm đích
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            PoweredWireUI poweredWireUI = eventData.pointerDrag.GetComponent<PoweredWireUI>();

            if (poweredWireUI != null)
            {
                PoweredWireStats1 poweredWireS = poweredWireUI.powerWireS;
                poweredWireS.connected = false;
                unpowerdWireS.connected = false;
            }
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