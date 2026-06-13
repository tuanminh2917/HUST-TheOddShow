using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item Dropped into Slot");

        if (eventData.pointerDrag != null)
        {
            RectTransform itemRect = eventData.pointerDrag.GetComponent<RectTransform>();

            // KIỂM TRA: itemRect hợp lệ VÀ ô slot này hiện đang TRỐNG (không có vật phẩm nào bên trong)
            if (itemRect != null && GetComponentInChildren<DragDrop>() == null)
            {
                // 1. Đổi cha của Item thành chính Slot này (Giải quyết vấn đề đồng cấp)
                itemRect.SetParent(transform);

                // 2. Vì đã là con của Slot, anchoredPosition = zero sẽ đưa Item vào CHÍNH GIỮA Slot
                itemRect.anchoredPosition = Vector2.zero;

                // 3. Giảm scale xuống 0.80 khi nằm gọn trong slot
                itemRect.localScale = new Vector3(0.80f, 0.80f, 1f);
            }
        }
    }
}