using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }

    [Header("UI Canvas bên phải")]
    [SerializeField] private Transform rightScreenContainer; // Nơi chứa câu đố

    private GameObject currentPuzzleInstance;
    private PuzzleData currentPuzzleData;

    private void Awake()
    {
        // Khởi tạo Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Hàm mở câu đố khi Player đến gần Activator
    public void OpenPuzzle(PuzzleData data)
    {
        if (currentPuzzleInstance != null) CloseCurrentPuzzle();

        currentPuzzleData = data;

        // Sinh ra giao diện câu đố ở vùng màn hình bên phải
        currentPuzzleInstance = Instantiate(data.puzzlePrefab, rightScreenContainer);
    }

    // Hàm đóng câu đố khi Player đi xa
    public void CloseCurrentPuzzle()
    {
        if (currentPuzzleInstance != null)
        {
            Destroy(currentPuzzleInstance);
            currentPuzzleInstance = null;
            currentPuzzleData = null;
        }
    }

    // Hàm này sẽ được gọi từ chính câu đố khi người chơi giải xong
    public void CompletePuzzle()
    {
        if (currentPuzzleData != null)
        {
            currentPuzzleData.isSolved = true;
            Debug.Log($"Đã giải xong câu đố: {currentPuzzleData.puzzleID}");

            // Chạy hiệu ứng animation hoàn thành ở đây nếu có...

            CloseCurrentPuzzle();
        }
    }
}