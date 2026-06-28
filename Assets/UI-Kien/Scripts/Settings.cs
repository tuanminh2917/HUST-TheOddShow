using UnityEngine;
using UnityEngine.UIElements;

public class Settings : MonoBehaviour
{
    private VisualElement background;
    private Label settingsTitle;
    private VisualElement sliderSfx;
    private VisualElement sliderBgm;
    private VisualElement btnRow;
    private Button resumeBtn;
    private Button quitBtn;

    private float time;

    void OnEnable()
    {
        var doc = GetComponent<UIDocument>();
        if (doc == null)
        {
            Debug.LogError("Settings: UIDocument not found on GameObject. Attach a UI Document first.");
            enabled = false;
            return;
        }

        var root = doc.rootVisualElement;

        background = root.Q<VisualElement>("Background");
        settingsTitle = root.Q<Label>("settings-title");
        sliderSfx = root.Q<VisualElement>("slider-sfx");
        sliderBgm = root.Q<VisualElement>("slider-bgm");
        btnRow = root.Q<VisualElement>("button-row");
        resumeBtn = root.Q<Button>("resume-btn");
        quitBtn = root.Q<Button>("quit-btn");

        if (settingsTitle != null)
        {
            settingsTitle.style.opacity = 0f;
            settingsTitle.style.translate = new Translate(0, -20);
        }

        if (sliderSfx != null) sliderSfx.style.opacity = 0.95f;
        if (sliderBgm != null) sliderBgm.style.opacity = 0.95f;

        RegisterHover(resumeBtn);
        RegisterHover(quitBtn);

        if (resumeBtn != null) resumeBtn.clicked += OnResumeClicked;
        if (quitBtn != null) quitBtn.clicked += OnQuitClicked;
    }

    void OnDisable()
    {
        if (resumeBtn != null) resumeBtn.clicked -= OnResumeClicked;
        if (quitBtn != null) quitBtn.clicked -= OnQuitClicked;
    }

    void Update()
    {
        time += Time.deltaTime;

        // Fade in title
        if (settingsTitle != null)
        {
            float alpha = Mathf.Clamp01(time / 0.9f);
            settingsTitle.style.opacity = alpha;
            settingsTitle.style.translate = new Translate(0, Mathf.Lerp(-20f, 0f, alpha));
        }

        // Pulse sliders
        AnimateSlider(sliderSfx, 4f, 0.02f);
        AnimateSlider(sliderBgm, 4.2f, 0.02f);

        // Pulse button row opacity slightly
        if (btnRow != null)
        {
            float pulse = 0.12f + Mathf.Sin(time * 1.8f) * 0.04f;
            btnRow.style.opacity = 0.85f + pulse;
        }
    }

    private void AnimateSlider(VisualElement slider, float speed, float magnitude)
    {
        if (slider == null) return;
        float s = 1f + Mathf.Sin(time * speed) * magnitude;
        slider.style.scale = new Scale(new Vector3(s, s, 1f));
    }

    private void RegisterHover(Button btn)
    {
        if (btn == null) return;

        btn.RegisterCallback<PointerEnterEvent>(_ =>
        {
            btn.style.scale = new Scale(Vector3.one * 1.05f);
            btn.style.backgroundColor = new StyleColor(new UnityEngine.Color(0.7f, 0.1f, 0.1f, 0.5f));
        });

        btn.RegisterCallback<PointerLeaveEvent>(_ =>
        {
            btn.style.scale = new Scale(Vector3.one);
            btn.style.backgroundColor = new StyleColor(new UnityEngine.Color(0f, 0f, 0f, 0.35f));
        });
    }

    private void OnResumeClicked()
    {
        Debug.Log("Resume clicked");
        // TODO: đóng settings hoặc unpause game
    }

    private void OnQuitClicked()
    {
        Debug.Log("Quit clicked");
        // TODO: xử lý thoát về menu chính hoặc hiện confirm
    }
}
