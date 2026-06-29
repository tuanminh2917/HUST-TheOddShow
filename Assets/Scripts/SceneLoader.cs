using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    public Animator transition;
    public float transitionTime = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        // Đăng ký sự kiện: Mỗi khi scene được load xong thì gọi hàm OnSceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Hủy đăng ký để tránh rò rỉ bộ nhớ (Memory Leak)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Hàm này sẽ TỰ ĐỘNG CHẠY ngay khi scene mới vừa load xong
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (transition != null)
        {
            // Reset lại Trigger "Start" cũ (nếu có) để tránh bị kẹt
            transition.ResetTrigger("Start");

            // Kích hoạt Trigger để làm sáng màn hình lại (Fade In)
            // Hãy đổi tên "End" thành tên Trigger mở màn hình trong Animator của bạn
            transition.SetTrigger("End");
        }
    }

    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        if (transition != null)
        {
            transition.SetTrigger("Start"); // Làm tối màn hình (Fade Out)
        }

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}