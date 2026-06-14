using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

// Kế thừa các Interface UI để xử lý Kéo/Thả và Hover chuột
public class PoweredWireUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public PoweredWireStats1 powerWireS;
    private LineRenderer line;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    void Start()
    {
        powerWireS = gameObject.GetComponent<PoweredWireStats1>();
        line = gameObject.GetComponentInParent<LineRenderer>();

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        // Thêm CanvasGroup để xử lý việc "xuyên thấu" Raycast khi drag
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();

        // Lưu vị trí UI ban đầu (Nhớ đổi startPosition trong script Stats thành Vector3 hoặc Vector2)
        powerWireS.startPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        if (line != null)
        {
            // Cập nhật vị trí các đốt dây dựa trên RectTransform local position
            line.SetPosition(3, new Vector3(rectTransform.localPosition.x - 10f, rectTransform.localPosition.y, 0));
            line.SetPosition(2, new Vector3(rectTransform.localPosition.x - 40f, rectTransform.localPosition.y, rectTransform.localPosition.z));
        }
    }

    // Tương đương OnPointerEnter (Chuột hover vào đầu dây)
    public void OnPointerEnter(PointerEventData eventData)
    {
        powerWireS.movable = true;
    }

    // Tương đương OnMouseExit
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!powerWireS.moving)
        {
            powerWireS.movable = false;
        }
    }

    // Khi bắt đầu click và kéo dây
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!powerWireS.movable) return;

        powerWireS.moving = true;

        // QUAN TRỌNG: Tắt blocksRaycasts để chuột có thể "nhìn xuyên" qua đầu dây này, 
        // từ đó mới tương tác được với điểm đích (UnpowerWire) ở phía dưới.
        canvasGroup.blocksRaycasts = false;
    }

    // Khi đang kéo dây (Thay thế cho MoveWire cũ)
    public void OnDrag(PointerEventData eventData)
    {
        if (!powerWireS.moving) return;

        // Di chuyển UI Element mượt mà theo chuột, bất kể độ phân giải màn hình hay Canvas Scale
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Khi thả chuột ra (Tương đương OnMouseUp)
    public void OnEndDrag(PointerEventData eventData)
    {
        powerWireS.moving = false;
        canvasGroup.blocksRaycasts = true; // Bật lại Raycast để có thể kéo tiếp lần sau

        if (!powerWireS.connected)
            rectTransform.anchoredPosition = powerWireS.startPosition;
        else
            rectTransform.anchoredPosition = powerWireS.connectedPosition;
    }
}