using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnpoweredWireUI : MonoBehaviour, IDropHandler
{
    private UnpowerWireStats unpoweredWireS;

    private RectTransform rectTransform;

    private void Awake()
    {
        unpoweredWireS = GetComponent<UnpowerWireStats>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            RectTransform itemRect = eventData.pointerDrag.GetComponent<RectTransform>();
            PoweredWireStats poweredWireStats = eventData.pointerDrag.GetComponent<PoweredWireStats>();

            if (poweredWireStats != null) {
                Debug.Log("Không tìm thấy poweredWireStats");
            }

            if (poweredWireStats.objectColor != unpoweredWireS.objectColor) return;

            else
            {
                poweredWireStats.connected = true;
                unpoweredWireS.connected = true;
                poweredWireStats.connectedPosition = rectTransform.anchoredPosition;

                // Turn On light
                unpoweredWireS.poweredLight.SetActive(true);
            }
        }
    }

}
