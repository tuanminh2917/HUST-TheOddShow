using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GeneticPuzzleManager : MonoBehaviour
{
    [Header("Puzzle Settings")]
    // Mảng chứa câu hỏi (các cặp sprite)
    public List<Sprite> questionsSprite;
    public string[] questionsText;
    // Mảng chứa đáp án đúng tương ứng với từng giai đoạn
    public string[] correctAnswers;
    public List<Cell> allCells;

    public Image image0;
    public Image image1;
    public TMPro.TextMeshProUGUI text0;
    public TMPro.TextMeshProUGUI text1;

    private int currentStage = 0;

    private void Start()
    {
        ChangeQuestion();
    }


    public void CheckCell(Cell droppedCell)
    {
        // Kiểm tra phenotype của tế bào có khớp với đáp án giai đoạn hiện tại không
        if (droppedCell.Phenotype == correctAnswers[currentStage])
        {
            Debug.Log($"Đúng! Hoàn thành giai đoạn {currentStage + 1}");

            droppedCell.gameObject.SetActive(false);
            currentStage++;

            if (currentStage >= correctAnswers.Length)
            {
                Debug.Log("Chúc mừng! Bạn đã giải xong câu đố di truyền.");
                // Thêm logic hoàn thành câu đố ở đây
            }
            else
            {
                ChangeQuestion();
            }
        }
        else
        {
            Debug.Log("Sai gene! Câu đố đã bị reset.");
            ResetPuzzle();
        }
    }

    private void ResetPuzzle()
    {
        currentStage = 0;

        // Khôi phục lại toàn bộ tế bào
        foreach (Cell cell in allCells)
        {
            cell.ResetToInitialState();
        }

        // Hiển thị lại câu hỏi đầu tiên
        ChangeQuestion();
    }

    private void ChangeQuestion()
    {
        if (currentStage < questionsSprite.Count)
        {
            image0.sprite = questionsSprite[currentStage * 2];
            image1.sprite = questionsSprite[currentStage * 2 + 1];
            text0.text = questionsText[currentStage * 2];
            text1.text = questionsText[currentStage * 2 + 1];

            if (text0.text.Contains("c"))
            {
                // Thêm logic xử lý khi text0 chứa "c"
                // Giảm kích thước image0 để tạo hiệu ứng "c" nhỏ hơn (width=height=85)
                image0.rectTransform.sizeDelta = new Vector2(85, 85);
            }
            else
            {
                // Khôi phục kích thước mặc định nếu text0 không chứa "c"
                image0.rectTransform.sizeDelta = new Vector2(168, 168);
            }

            if (text1.text.Contains("c"))
            {
                // Thêm logic xử lý khi text1 chứa "c"
                // Giảm kích thước image1 để tạo hiệu ứng "c" nhỏ hơn (width=height=85)
                image1.rectTransform.sizeDelta = new Vector2(85, 85);
            }
            else
            {
                // Khôi phục kích thước mặc định nếu text1 không chứa "c"
                image1.rectTransform.sizeDelta = new Vector2(168, 168);
            }
        }
    }
}