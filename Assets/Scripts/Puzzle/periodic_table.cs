using UnityEngine;
using UnityEngine.EventSystems;

public class periodic_table : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    public Canvas canvas;
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        // Lấy RectTransform của chính ảnh này
        rectTransform = GetComponent<RectTransform>();

        // Thêm CanvasGroup nếu bạn muốn làm mờ ảnh khi đang kéo (không bắt buộc)
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Giảm alpha để tạo hiệu ứng đang kéo (tùy chọn)
        canvasGroup.alpha = 0.6f;
        // Cho phép xuyên qua raycast để không block các UI phía dưới nếu cần thả vào ô chứa
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Di chuyển vị trí dựa trên delta chuột/ngón tay và scale của Canvas
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Khôi phục lại trạng thái ban đầu
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}