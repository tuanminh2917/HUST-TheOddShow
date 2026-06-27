using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioData", menuName = "Audio/Audio Data")]
public class AudioData : ScriptableObject
{
    [Header("Cấu hình cơ bản")]
    public string audioName; // Tên định danh để gọi phát âm thanh
    public AudioClip audioClip;
    public AudioType audioType;

    [Header("Cài đặt thuộc tính")]
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    public bool loop = false; // Thường bật cho BGM
}