using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Events; // 1. Thêm thư viện này để dùng UnityEvent

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    
    [TextArea(3, 5)]
    public string[] lines;
    public float textSpeed;

    [Header("Events")]
    // 2. Tạo sự kiện khi kết thúc hội thoại
    public UnityEvent onDialogueComplete;

    private int index;
    private Coroutine typeLineCoroutine;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                if (typeLineCoroutine != null) StopCoroutine(typeLineCoroutine);
                AudioManager.Instance.StopAllSFX(); // Dừng âm thanh gõ khi người chơi nhấn chuột
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        if (lines.Length > 0)
        {
            typeLineCoroutine = StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine()
    {
        AudioManager.Instance.Play("write");
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        AudioManager.Instance.StopAllSFX();
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            typeLineCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            // 3. Kích hoạt tất cả các hàm được gán trong Inspector khi hết câu
            if (onDialogueComplete != null)
            {
                onDialogueComplete.Invoke();
            }

            gameObject.SetActive(false);
        }
    }
}