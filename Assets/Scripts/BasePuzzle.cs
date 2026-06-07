using UnityEngine;

public class BasePuzzle : MonoBehaviour
{
    // Gọi hàm này trong code logic riêng của bạn khi người chơi hoàn thành câu đố
    protected void FinishPuzzle()
    {
        PuzzleManager.Instance.CompletePuzzle();
    }
}