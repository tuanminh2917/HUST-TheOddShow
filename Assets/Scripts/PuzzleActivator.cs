using UnityEngine;

public class PuzzleActivator : MonoBehaviour
{
    [Header("Cấu hình câu đố")]
    [SerializeField] private PuzzleData puzzleData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu là Người chơi và câu đố này chưa được giải
        if (other.CompareTag("Player") && puzzleData != null && !puzzleData.isSolved)
        {
            PuzzleManager.Instance.OpenPuzzle(puzzleData);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PuzzleManager.Instance.CloseCurrentPuzzle();
        }
    }
}