using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

using System.Collections;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> slotList;
    [SerializeField] private TMP_Text itemName;

    // Biến để lưu vết bộ đếm thời gian đang chạy
    private Coroutine clearNameCoroutine;

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
            CheckClickedSlot();
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
}