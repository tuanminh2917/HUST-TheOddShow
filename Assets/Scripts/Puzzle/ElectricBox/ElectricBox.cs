using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

// 1. SỬA: Kế thừa thêm MonoBehaviour và tạm xóa IPointerClickHandler nếu không dùng tới click
public class ElectricBox : MonoBehaviour, IDropHandler
{
    private bool isConnected = false;

    public GameObject passwordScreen;

    // 2. SỬA: Đổi sang UGUI để tương thích với hệ Canvas 2D của bạn
    public TextMeshProUGUI textMeshPro;

    // 3. SỬA: Thêm từ khóa 'public' cho hàm OnDrop
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        if (eventData.pointerDrag.TryGetComponent<DragDrop>(out DragDrop dragDrop))
        {
            string id = dragDrop.id;
            if (string.IsNullOrEmpty(id) || isConnected) return;

            // 4. SỬA: Chuyển '=' thành '==' để làm phép so sánh
            if (id == "Copper")
            {
                textMeshPro.text = "Tủ điện đã được kết nối.";
                passwordScreen.SetActive(true);

                // 5. BỔ SUNG: Khóa trạng thái lại để không nhận thêm đồ nữa
                isConnected = true;

                // 6. Tùy chọn: Xóa vật phẩm Copper đi sau khi đã dùng xong (giống cơ chế đĩa Petri)
                Destroy(eventData.pointerDrag);
                Debug.Log("Kết nối tủ điện thành công bằng vật liệu Copper!");
            }
        }
    } // Đóng ngoặc hàm OnDrop
} // Đóng ngoặc class ElectricBox