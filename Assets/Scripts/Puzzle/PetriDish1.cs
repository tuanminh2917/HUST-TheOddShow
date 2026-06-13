using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PetriDish1 : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public enum DishState
    {
        Empty = 0,
        HasCuO = 1,
        HasAlcohol = 2,
        HasBoth = 3,
        Lit = 4,
        Reacted = 5
    }

    [Header("Trạng thái hiện tại")]
    [SerializeField] private DishState currentState = DishState.Empty;

    [Header("Cấu hình Giao diện")]
    public List<Sprite> spriteStages;
    private Image image;

    [Header("Phần thưởng câu đố")]
    public GameObject present; // Prefab UI của phần thưởng
    private bool isRewardSpawned = false; // Flag chặn click spam quà
    private void Start()
    {
        image = GetComponent<Image>();
        UpdateSprite();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        if (eventData.pointerDrag.TryGetComponent<DragDrop>(out DragDrop dragDrop))
        {
            string id = dragDrop.id;
            if (string.IsNullOrEmpty(id)) return;

            bool isIngredientConsumed = false;

            // Kiểm tra tính hợp lệ của vật phẩm theo trạng thái
            switch (currentState)
            {
                case DishState.Empty:
                    if (id == "CuO") { currentState = DishState.HasCuO; isIngredientConsumed = true; }
                    else if (id == "Alcohol") { currentState = DishState.HasAlcohol; isIngredientConsumed = true; }
                    break;

                case DishState.HasCuO:
                    if (id == "Alcohol") { currentState = DishState.HasBoth; isIngredientConsumed = true; }
                    break;

                case DishState.HasAlcohol:
                    if (id == "CuO") { currentState = DishState.HasBoth; isIngredientConsumed = true; }
                    break;

                case DishState.HasBoth:
                    if (id == "Match") { currentState = DishState.Lit; isIngredientConsumed = true; }
                    break;
            }

            // ĐÚNG THỨ TỰ & ĐÚNG CHẤT: Tiêu thụ vật phẩm
            if (isIngredientConsumed)
            {
                UpdateSprite();
                Destroy(eventData.pointerDrag);
                Debug.Log($"Đã thêm thành công {id} vào đĩa.");
            }
            else
            {
                // SAI THỨ TỰ / SAI CHẤT: Không làm gì cả! 
                // Vật phẩm sẽ tự động bay về vị trí cũ nhờ OnEndDrag của DragDrop.cs
                Debug.LogWarning($"Vật phẩm {id} không hợp lệ ở trạng thái {currentState}! Trả về chỗ cũ.");
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentState == DishState.Lit)
        {
            currentState = DishState.Reacted;
            UpdateSprite();
        }

        // Bước 2: Sinh ra phần thưởng UI (Có kiểm tra xem đã sinh ra chưa)
        if (currentState == DishState.Reacted && !isRewardSpawned)
        {
            isRewardSpawned = true; // Khóa lại ngay lập tức, click lần sau sẽ không vào đây nữa

            // Giải pháp UI: Ép phần thưởng mới tạo phải làm con của cùng một khung chứa với đĩa Petri (transform.parent)
            GameObject spawnedPresent = Instantiate(present, transform.parent);

            // Bước 3: Reset lại tọa độ và tỉ lệ hiển thị cho UI Object mới
            RectTransform presentRect = spawnedPresent.GetComponent<RectTransform>();
            if (presentRect != null)
            {
                // Đặt phần thưởng xuất hiện tại ĐÚNG VỊ TRÍ của đĩa Petri trên màn hình
                presentRect.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                // Ép scale về chuẩn 1 (Tránh việc UI bị phóng to/thu nhỏ dị thường do ảnh hưởng từ cha)
                presentRect.localScale = Vector3.one;
            }

            Debug.Log("Phần thưởng UI đã xuất hiện thành công trên Canvas!");
        }
    }

    private void UpdateSprite()
    {
        int index = (int)currentState;
        if (spriteStages != null && index < spriteStages.Count)
        {
            image.sprite = spriteStages[index];
        }
    }
}