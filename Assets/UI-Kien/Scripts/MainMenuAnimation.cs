using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuAnimation : MonoBehaviour
{
    private VisualElement background;
    private Label titleLabel;
    private Label subtitleLabel;
    private Button continueBtn;
    private Button newGameBtn;
    private VisualElement cornerTL;
    private VisualElement cornerTR;
    private VisualElement cornerBL;
    private VisualElement cornerBR;

    private float time;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        background = root.Q<VisualElement>("Background");
        titleLabel = root.Q<Label>("title-label");
        subtitleLabel = root.Q<Label>("subtitle-label");
        continueBtn = root.Q<Button>("continue-btn");
        newGameBtn = root.Q<Button>("newgame-btn");
        cornerTL = root.Q<VisualElement>("ConrnerTL");
        cornerTR = root.Q<VisualElement>("ConrnerTR");
        cornerBL = root.Q<VisualElement>("ConrnerBL");
        cornerBR = root.Q<VisualElement>("ConrnerBR");

        if (titleLabel != null)
        {
            titleLabel.style.opacity = 0f;
            titleLabel.style.translate = new Translate(0, -30);
        }

        if (subtitleLabel != null)
        {
            subtitleLabel.style.opacity = 0f;
            subtitleLabel.style.translate = new Translate(0, -15);
        }

        RegisterHover(continueBtn);
        RegisterHover(newGameBtn);
    }

    void Update()
    {
        time += Time.deltaTime;

        // Fade in title and subtitle
        if (titleLabel != null)
        {
            float alpha = Mathf.Clamp01(time / 1.2f);
            titleLabel.style.opacity = alpha;
            titleLabel.style.translate = new Translate(0, Mathf.Lerp(-30, 0, alpha));
        }

        if (subtitleLabel != null)
        {
            float alpha = Mathf.Clamp01((time - 0.3f) / 1.2f);
            subtitleLabel.style.opacity = alpha;
            subtitleLabel.style.translate = new Translate(0, Mathf.Lerp(-15, 0, alpha));
        }

        // Animate corners subtly
        AnimateCorner(cornerTL, 0.5f);
        AnimateCorner(cornerTR, 0.7f);
        AnimateCorner(cornerBL, 0.9f);
        AnimateCorner(cornerBR, 1.1f);
    }

    private void AnimateCorner(VisualElement corner, float offset)
    {
        if (corner == null) return;

        float pulse = 1f + Mathf.Sin((time + offset) * 2f) * 0.03f;
        corner.style.scale = new Scale(new Vector3(pulse, pulse, 1));
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
}
