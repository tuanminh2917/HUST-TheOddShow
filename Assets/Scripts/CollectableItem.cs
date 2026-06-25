using UnityEngine;
using UnityEngine.EventSystems;

// Kế thừa IPointerClickHandler để bắt sự kiện click chuột/tab tay trên UI
public class CollectableItem : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Tìm kiếm script Inventory đang có trong Scene (Chuẩn Unity 6)
        Inventory inventory = Object.FindAnyObjectByType<Inventory>();

        if (inventory != null)
        {
            // Hỏi kho đồ xem còn ô nào trống không
            GameObject emptySlot = inventory.GetEmptySlot();

            if (emptySlot != null)
            {
                Debug.Log($"Đã nhặt {gameObject.name} vào kho đồ!");

                // 1. Chuyển Object cha của vật phẩm về ô trống vừa tìm được
                transform.SetParent(emptySlot.transform);

                // 2. Căn giữa vật phẩm vào trong ô slot đó
                RectTransform rectTransform = GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = Vector2.zero;
                    rectTransform.localScale = Vector3.one; // Reset scale về chuẩn 1
                }

                // 3. Tắt script này đi để khi đã ở trong kho đồ, 
                // người chơi click vào sẽ đổi sang tính năng hiện tên hiển thị thay vì đi "nhặt" tiếp
                this.enabled = false;
            }
            else
            {
                Debug.LogWarning("Không thể nhặt! Kho đồ đã đầy chỗ.");
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy hệ thống Inventory trong Scene!");
        }
        // GỌI DÒNG NÀY: Hoàn thành hướng dẫn click
        TutorialManager.Instance.CompleteTutorial(TutorialType.ClickableItem);
    }
}