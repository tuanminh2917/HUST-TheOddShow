using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

// 1. SỬA: Kế thừa thêm MonoBehaviour và tạm xóa IPointerClickHandler nếu không dùng tới click
public class ElectricBox : MonoBehaviour, IDropHandler
{
    public bool isConnected = false;
    private bool hasCopper = false;
    private bool hasCheckedOnce = false;
    private bool hasCheckedTwice = false;

    public GameObject electricBoxPuzzleLogic;

    public GameObject electricBoxPuzzle;

    public ComputerController computerController;

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
                hasCopper = true;
                Destroy(eventData.pointerDrag);
                electricBoxPuzzleLogic.SetActive(true);
                electricBoxPuzzle.SetActive(false);

                //Debug.Log("Kết nối tủ điện thành công bằng vật liệu Copper!");
            }
        }
    } // Đóng ngoặc hàm OnDrop

    private void Update()
    {
        if (!hasCopper) return;
        else if (!isConnected && hasCopper)
        {
            if (!hasCheckedOnce)
            {
                hasCheckedOnce = true;
                electricBoxPuzzleLogic.SetActive(true);
                electricBoxPuzzle.SetActive(false);
            }
        }
        else if (isConnected && hasCopper)
        {
            if (!hasCheckedTwice)
            {
                hasCheckedTwice = true;
                computerController.solvable = true;
                textMeshPro.text = "Tủ điện đã được kết nối.";
            }
        }
    }
} // Đóng ngoặc class ElectricBox