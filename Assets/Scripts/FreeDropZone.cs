using UnityEngine;
using UnityEngine.EventSystems;

public class FreeDropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item dropped freely sth");

        if (eventData.pointerDrag != null)
        {
            RectTransform itemRect = eventData.pointerDrag.GetComponent<RectTransform>();

            if (itemRect != null)
            {
                // Đổi cha sang cái Bàn, nhưng giữ nguyên vị trí hiện tại (world position stays = true)
                // Item sẽ nằm yên tại đúng vị trí bạn thả chuột
                itemRect.SetParent(transform, true);

                // Thu nhỏ scale lại còn 0.95 như bạn muốn
                itemRect.localScale = new Vector3(0.95f, 0.95f, 1f);
            }
        }
    }
}