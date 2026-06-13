using UnityEngine;
using UnityEngine.UI;

// This class represents a cell in the petri dish. It has a phenotype that can be set in the inspector.
// Convetion:
// - Phenotype (Kiểu hình): A combination of traits (Sự kết hợp của các đặc điểm):
// - Color (Màu sắc): Red/Green (Đỏ/Xanh): A/a
// - Cell wall (Thành tế bào): Smooth/Wrinkled (Trơn/Nhăn): B/b
// - Size (Kích thước): Large/Small (Lớn/Nhỏ): C/c

[RequireComponent(typeof(RectTransform))]
public class Cell : MonoBehaviour
{
    public string Phenotype;

    private float dragRangeHeight = 768f; // Giới hạn kéo thả theo chiều dọc (có thể điều chỉnh tùy theo thiết kế UI)
    private float dragRangeWidth = 668f; // Giới hạn kéo thả theo chiều ngang (có thể điều chỉnh tùy theo thiết kế UI)

    private RectTransform rectTransform;
    private Vector2 startAnchoredPosition;
    private Transform startParent; // Rất quan trọng trong UI Drag & Drop
    private DragDrop dragDropComponent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        dragDropComponent = GetComponent<DragDrop>();
    }

    void Start()
    {
        // Ghi nhớ vị trí và node cha xuất phát
        startAnchoredPosition = rectTransform.anchoredPosition;
        startParent = transform.parent;
    }

    private void Update()
    {
        // Giới hạn vị trí kéo thả trong phạm vi đã định
        Vector2 clampedPosition = rectTransform.anchoredPosition;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -dragRangeWidth / 2, dragRangeWidth / 2);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -dragRangeHeight / 2, dragRangeHeight / 2);
        rectTransform.anchoredPosition = clampedPosition;
    }
    public void ResetToInitialState()
    {
        gameObject.SetActive(true);

        dragDropComponent.canvasGroup.blocksRaycasts = true; // Cho phép tương tác lại với tế bào sau khi reset

        // Phải SetParent trước khi set anchoredPosition để tọa độ không bị sai lệch
        transform.SetParent(startParent);
        rectTransform.anchoredPosition = startAnchoredPosition;
    }
}
