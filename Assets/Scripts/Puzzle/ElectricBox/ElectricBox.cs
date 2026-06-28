using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

// 1. SỬA: Kế thừa thêm MonoBehaviour và tạm xóa IPointerClickHandler nếu không dùng tới click
public class ElectricBox : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public bool isConnected = false;
    private bool hasCopper = false;
    private bool hasCheckedOnce = false;
    private bool hasCheckedTwice = false;

    public GameObject electricBoxPuzzleLogic;

    public ComputerController computerController;

    // 2. SỬA: Đổi sang UGUI để tương thích với hệ Canvas 2D của bạn
    public TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro.gameObject.SetActive(false);
    }

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

    public void OnPointerClick(PointerEventData eventData) {
        if (!isConnected) StartCoroutine(NotConnectedCoroutine());
        else StartCoroutine(ConnectedCoroutine());
    }

    private IEnumerator NotConnectedCoroutine()
    {
        AudioManager.Instance.Play("electric crackle");
        textMeshPro.text = "Đứt dây";
        textMeshPro.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        textMeshPro.gameObject.SetActive(false);
    }

    private IEnumerator ConnectedCoroutine()
    {
        textMeshPro.text = "Đã kết nối";
        textMeshPro.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        textMeshPro.gameObject.SetActive(false);
    }
} 