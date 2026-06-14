using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PasswordScreen : MonoBehaviour
{
    [SerializeField] private string password = "131783";

    public List<Button> digitButtons;
    public List<Button> functionalButtons;

    // THAY ĐỔI: Chuyển hẳn thành kiểu TMP_InputField để nhận diện cả ô nhập liệu
    public TMP_InputField passwordInputField;

    public GameObject mainScreen;

    void Start()
    {
        if (passwordInputField != null)
        {
            passwordInputField.text = ""; // Xóa trống ban đầu
        }

        // Đăng ký phím số
        foreach (Button btn in digitButtons)
        {
            if (btn == null) continue;
            Button capturedButton = btn;
            capturedButton.onClick.AddListener(() => OnDigitClicked(capturedButton));
        }

        // Đăng ký phím Delete
        if (functionalButtons != null && functionalButtons.Count > 0 && functionalButtons[0] != null)
        {
            functionalButtons[0].onClick.AddListener(OnDeleteClicked);
        }

        // Đăng ký phím GO
        if (functionalButtons != null && functionalButtons.Count > 1 && functionalButtons[1] != null)
        {
            functionalButtons[1].onClick.AddListener(OnGoClicked);
        }
    }

    private void OnDigitClicked(Button clickedButton)
    {
        if (passwordInputField == null) return;

        TMP_Text buttonText = clickedButton.GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            passwordInputField.text += buttonText.text.Trim();
        }
    }

    private void OnDeleteClicked()
    {
        if (passwordInputField == null || string.IsNullOrEmpty(passwordInputField.text)) return;

        passwordInputField.text = passwordInputField.text.Substring(0, passwordInputField.text.Length - 1);
    }

    private void OnGoClicked()
    {
        if (passwordInputField == null) return;

        if (passwordInputField.text == password)
        {
            Debug.Log("Mật khẩu nhập: " + passwordInputField.text);
            Debug.Log("Mật khẩu đúng: " + password);

            Debug.Log("Mật khẩu chính xác!");
            mainScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Sai mật khẩu!");
            passwordInputField.text = "";
        }
    }
}