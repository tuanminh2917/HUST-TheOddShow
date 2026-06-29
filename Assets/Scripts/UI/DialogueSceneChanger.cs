using UnityEngine;

public class DialogueSceneChanger : MonoBehaviour
{
    public string targetSceneName;

    // Hàm này sẽ được gọi bởi UnityEvent của Dialogue
    public void ChangeScene()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadNextScene(targetSceneName);
        }
        else
        {
            Debug.LogError("Chưa có SceneLoader Instance trong Game!");
        }
    }
}