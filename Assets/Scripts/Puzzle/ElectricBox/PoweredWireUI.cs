using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PoweredWireUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private PoweredWireStats poweredWireS;

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    public CanvasGroup canvasGroup;

    [Header("UI Wire Settings")]
    [SerializeField] private RectTransform wireImageRect; // Kéo RectTransform của cái ảnh dây điện (Wire_Graphic) vào đây

    // Bổ sung thêm 1 biến private ở đầu class để lưu khoảng lệch khi mới click chuột
    private Vector2 dragOffset;

    private void Awake()
    {
        poweredWireS = GetComponent<PoweredWireStats>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        poweredWireS.movable = true;

        // 1. Ghi nhớ vị trí và gốc gác cũ trước khi kéo
        poweredWireS.startPosition = rectTransform.anchoredPosition;

        // Đặt vị trí gốc của sợi dây trùng với vị trí của đầu nút bấm này
        if (wireImageRect != null)
        {
            wireImageRect.anchoredPosition = poweredWireS.startPosition;
        }

        rectTransform.localScale = Vector3.one;
    }

    private void Update()
    {
        // Luôn cập nhật chiều dài và góc xoay của dây theo vị trí hiện tại của đầu nút
        if (wireImageRect != null)
        {
            UpdateWireGraphic();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        // TÍNH TOÁN OFFSET BAN ĐẦU:
        // Giúp đầu dây không bị "giật" (snap) tâm về con trỏ chuột nếu người chơi click lệch vào mép nút dây
        RectTransform parentRect = rectTransform.parent as RectTransform;
        if (parentRect != null)
        {
            Vector2 localMousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                eventData.position,
                eventData.pressEventCamera,
                out localMousePos
            );
            // Lưu khoảng cách lệch giữa vị trí đầu dây và vị trí chuột lúc bấm xuống
            dragOffset = rectTransform.anchoredPosition - localMousePos;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!poweredWireS.movable) return;
        // Lấy RectTransform của đối tượng Cha (Parent) làm gốc tọa độ để tính toán
        RectTransform parentRect = rectTransform.parent as RectTransform;

        if (parentRect != null)
        {
            Vector2 localMousePos;

            // Hàm thần thánh: Chuyển vị trí chuột (eventData.position) thành tọa độ UI cục bộ chuẩn xác 100%
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                eventData.position,
                eventData.pressEventCamera, // Tự động xử lý chính xác dù là Canvas Overlay hay Camera
                out localMousePos
            );

            // Cập nhật vị trí đầu dây đi theo chuột một cách tuyệt đối (cộng thêm offset ban đầu)
            rectTransform.anchoredPosition = localMousePos + dragOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (!poweredWireS.connected)
        {
            rectTransform.anchoredPosition = poweredWireS.startPosition;
        }
        else {
            poweredWireS.connectedPosition = rectTransform.anchoredPosition;
        }

        poweredWireS.moving = false;

        //// BỔ SUNG: Nếu kết thúc kéo mà cha vẫn là Canvas 
        //// (Nghĩa là đĩa Petri từ chối nhận hoặc thả ra ngoài khoảng trống)
        //if (transform.parent == canvas.transform)
        //{
        //    // Trả món đồ về đúng vị trí cũ trong Inventory
        //    transform.SetParent(oldParent);
        //    rectTransform.anchoredPosition = oldPosition;
        //}
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }

    void UpdateWireGraphic()
    {
        Vector2 currentPos = rectTransform.anchoredPosition;

        // 1. Tính khoảng cách giữa điểm gốc và điểm hiện tại để làm độ dài (Width) của dây
        float distance = Vector2.Distance(poweredWireS.startPosition, currentPos);
        wireImageRect.sizeDelta = new Vector2(distance, wireImageRect.sizeDelta.y);

        // 2. Tính góc xoay (Angle) từ điểm gốc hướng về phía đầu nút đang kéo
        Vector2 direction = currentPos - poweredWireS.startPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Áp dụng góc xoay vào trục Z của UI Image
        wireImageRect.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
