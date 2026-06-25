using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class Girl : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject dialogue;

    private void Start()
    {
        dialogue.SetActive(false);
    }

    // Public so UI Buttons can call it
    public void OnPointerClick(PointerEventData eventData)
    {
        if (dialogue == null)
        {
            Debug.LogWarning("Girl.OnClick: dialogue reference is null.");
            return;
        }

        StartCoroutine(ShowDialogueAfterDelay());
        // GỌI DÒNG NÀY: Hoàn thành hướng dẫn click
        TutorialManager.Instance.CompleteTutorial(TutorialType.ClickableItem);
    }

    // Coroutine to activate dialogue in 3s
    private IEnumerator ShowDialogueAfterDelay()
    {
        dialogue.SetActive(true);
        yield return new WaitForSeconds(3f);
        dialogue.SetActive(false);
    }
}
