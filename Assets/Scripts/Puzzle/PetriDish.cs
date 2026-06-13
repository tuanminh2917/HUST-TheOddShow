using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PetriDish : MonoBehaviour, IDropHandler
{
    // Đã đổi kiểu dữ liệu tham chiếu thành GeneticPuzzleManager
    public GeneticPuzzleManager puzzleManager;

    //public Image image0;
    //public Image image1;
    //public TMPro.TextMeshProUGUI text0;
    //public TMPro.TextMeshProUGUI text1;

    private void Start()
    {
        // Đảm bảo rằng puzzleManager đã được gán trong Inspector
        if (puzzleManager == null)
        {
            Debug.LogError("GeneticPuzzleManager reference is not set in the PetriDish.");
        }

        //image0 = GetComponentsInChildren<Image>()[0];
        //image1 = GetComponentsInChildren<Image>()[1];
        //text0 = GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0];
        //text1 = GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1];
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<CanvasGroup>().alpha = 1f; // Restore the item's opacity
            Cell droppedCell = eventData.pointerDrag.GetComponent<Cell>();

            if (droppedCell != null)
            {
                puzzleManager.CheckCell(droppedCell);
            }
        }
    }
}