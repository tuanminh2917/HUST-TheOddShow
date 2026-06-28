using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public string id;

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    public CanvasGroup canvasGroup;

    // BỔ SUNG: Lưu lại vị trí và cha cũ để trả về khi làm sai
    private Transform oldParent;
    private Vector2 oldPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //AudioManager.Instance.Play("Click");

        Debug.Log("Begin Drag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        // 1. Ghi nhớ vị trí và gốc gác cũ trước khi kéo
        oldParent = transform.parent;
        oldPosition = rectTransform.anchoredPosition;

        rectTransform.localScale = Vector3.one;
        transform.SetParent(canvas.transform);

        // GỌI DÒNG NÀY: Hoàn thành hướng dẫn kéo vật phẩm ngay khi họ vừa bắt đầu kéo
        TutorialManager.Instance.CompleteTutorial(TutorialType.DraggableItem);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // BỔ SUNG: Nếu kết thúc kéo mà cha vẫn là Canvas 
        // (Nghĩa là đĩa Petri từ chối nhận hoặc thả ra ngoài khoảng trống)
        if (transform.parent == canvas.transform)
        {
            // Trả món đồ về đúng vị trí cũ trong Inventory
            transform.SetParent(oldParent);
            rectTransform.anchoredPosition = oldPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }
}