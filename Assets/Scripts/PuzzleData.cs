using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle Data", menuName = "TheHUSTOddShow/Puzzle Data")]
public class PuzzleData : ScriptableObject
{
    public string puzzleID;          // ID định danh duy nhất cho câu đố
    public GameObject puzzlePrefab;  // Prefab UI/Gameplay của câu đố sẽ hiện ở bên phải
    public bool isSolved;            // Trạng thái câu đố đã giải chưa
}