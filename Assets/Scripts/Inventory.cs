using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class Inventory : MonoBehaviour
{
    // --- SINGLETON PATTERN ---
    public static Inventory Instance { get; private set; }

    [SerializeField] private List<GameObject> slotList;
    [SerializeField] private TMP_Text itemName;

    // Biến để lưu vết bộ đếm thời gian đang chạy
    private Coroutine clearNameCoroutine;

    public GameObject popup;

    private void Awake()
    {
        // Khởi tạo Singleton và đảm bảo không bị trùng lặp
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Nếu muốn Inventory tồn tại xuyên suốt các Scene, bỏ comment dòng dưới:
        // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        slotList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }

        itemName = GetComponentInChildren<TMP_Text>();
        if (itemName != null)
        {
            itemName.text = "";
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //AudioManager.Instance.Play("Click");
            CheckClickedSlot();
        }

        if (CheckForItem("Key") || CheckForItem("key"))
        {
            FindFirstObjectByType<HintManager>().UpdateProgress(5);
            if (popup != null)
            {
                popup.SetActive(true); // Hiển thị popup
            }
            Time.timeScale = 0f; // Dừng trò chơi
        }
        else if (CheckForItem("Copper") || CheckForItem("copper"))
        {
            FindFirstObjectByType<HintManager>().UpdateProgress(1);
        }
    }

    // --- HÀM MỚI BỔ SUNG: Thêm item prefab vào slot trống ---
    public bool AddItemToEmptySlot(GameObject itemPrefab)
    {
        if (itemPrefab == null)
        {
            Debug.LogWarning("Prefab truyền vào bị null!");
            return false;
        }

        GameObject emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            // Sinh ra item mới và đặt nó làm con của slot trống tìm được
            GameObject newItem = Instantiate(itemPrefab, emptySlot.transform);

            // Đặt lại vị trí local về gốc tọa độ của Slot UI để nó nằm căn giữa slot
            newItem.transform.localPosition = Vector3.zero;

            Debug.Log($"Đã thêm thành công {itemPrefab.name} vào {emptySlot.name}");
            return true; // Thêm thành công
        }
        else
        {
            Debug.LogWarning("Kho đồ đã đầy, không thể thêm item!");
            return false; // Thêm thất bại do hết chỗ
        }
    }

    private void CheckClickedSlot()
    {
        if (EventSystem.current == null) return;

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            foreach (GameObject slot in slotList)
            {
                if (result.gameObject == slot || result.gameObject.transform.IsChildOf(slot.transform))
                {
                    DragDrop item = slot.GetComponentInChildren<DragDrop>();

                    if (item != null)
                    {
                        itemName.text = item.id;
                    }
                    else
                    {
                        itemName.text = "Empty";
                    }

                    // KÍCH HOẠT BỘ ĐẾM THỜI GIAN 3 GIÂY
                    StartClearNameTimer();

                    return;
                }
            }
        }
    }

    // Hàm tìm kiếm và trả về ô Slot đầu tiên còn trống trong kho đồ
    public GameObject GetEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            // Nếu ô slot này chưa chứa bất kỳ vật phẩm nào có script DragDrop
            if (slot.GetComponentInChildren<DragDrop>() == null)
            {
                return slot; // Trả về ô trống này
            }
        }
        return null; // Trả về null nếu tất cả các ô đã đầy
    }

    // Hàm quản lý việc khởi chạy bộ đếm thời gian
    private void StartClearNameTimer()
    {
        // Nếu trước đó đang có một bộ đếm 3s đang chạy ngầm, hãy hủy nó đi
        if (clearNameCoroutine != null)
        {
            StopCoroutine(clearNameCoroutine);
        }

        // Bắt đầu một bộ đếm 3 giây mới
        clearNameCoroutine = StartCoroutine(ClearNameAfterDelay(3f));
    }

    // Coroutine đếm ngược thời gian
    private IEnumerator ClearNameAfterDelay(float delay)
    {
        // Chờ đúng số giây được truyền vào (ở đây là 3 giây)
        yield return new WaitForSeconds(delay);

        // Sau khi chờ xong, xóa text về rỗng
        if (itemName != null)
        {
            itemName.text = "";
        }
    }

    // ĐÃ SỬA: Hàm trả về kiểu bool (true nếu tìm thấy item, false nếu không tìm thấy)
    private bool CheckForItem(string targetValue)
    {
        // Nếu không truyền gì vào hoặc chuỗi rỗng thì coi như không tìm thấy
        if (string.IsNullOrEmpty(targetValue)) return false;

        // Nếu popup đã hiển thị (trò chơi đã dừng), không cần quét lại và trả về false
        if (popup != null && popup.activeSelf) return false;

        foreach (GameObject slot in slotList)
        {
            if (slot != null)
            {
                DragDrop item = slot.GetComponentInChildren<DragDrop>();

                // Kiểm tra xem trong slot có item không, và ID hoặc Tên có khớp không
                if (item != null && (item.id == targetValue || item.gameObject.name == targetValue))
                {
                    //if (popup != null)
                    //{
                    //    popup.SetActive(true); // Hiển thị popup
                    //}

                    //Time.timeScale = 0f; // Dừng trò chơi

                    return true; // Trả về true và thoát hàm ngay lập tức (thay thế cho break)
                }
            }
        }

        return false; // Đi hết cả danh sách slot mà không thấy item nào khớp
    }
}