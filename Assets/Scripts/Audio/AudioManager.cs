using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource dialogueSource;

    [Header("Danh sách dữ liệu âm thanh")]
    [SerializeField] private List<AudioData> audioDataList;

    private Dictionary<string, AudioData> audioRegistry;

    // Lưu vết các Coroutine quản lý thời gian phát để tránh bị chồng chéo
    private Coroutine bgmDurationCoroutine;
    private Coroutine sfxDurationCoroutine;
    private Coroutine dialogueDurationCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeAudioSources();
        InitializeRegistry();
    }

    private void InitializeAudioSources()
    {
        if (bgmSource == null) bgmSource = gameObject.AddComponent<AudioSource>();
        if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();
        if (dialogueSource == null) dialogueSource = gameObject.AddComponent<AudioSource>();

        bgmSource.playOnAwake = false;
        sfxSource.playOnAwake = false;
        dialogueSource.playOnAwake = false;
    }

    private void InitializeRegistry()
    {
        audioRegistry = new Dictionary<string, AudioData>();
        foreach (var data in audioDataList)
        {
            if (data != null && !audioRegistry.ContainsKey(data.audioName))
            {
                audioRegistry.Add(data.audioName, data);
            }
        }
    }

    /// <summary>
    /// Phát âm thanh bằng tên định danh.
    /// startTime: Thời điểm bắt đầu phát (giây). Mặc định = 0f.
    /// duration: Thời gian phát (giây). Mặc định = -1f (phát đến hết clip).
    /// </summary>
    public void Play(string name, float startTime = 0f, float duration = -1f)
    {
        if (audioRegistry.TryGetValue(name, out AudioData data))
        {
            PlayAudio(data, startTime, duration);
        }
        else
        {
            Debug.LogWarning($"[AudioManager] Không tìm thấy âm thanh: {name}");
        }
    }

    /// <summary>
    /// Phát âm thanh bằng AudioData.
    /// </summary>
    public void PlayAudio(AudioData data, float startTime = 0f, float duration = -1f)
    {
        if (data == null || data.audioClip == null) return;

        // Kiểm tra nếu startTime vượt quá độ dài file âm thanh
        if (startTime >= data.audioClip.length)
        {
            Debug.LogWarning($"[AudioManager] startTime ({startTime}s) lớn hơn độ dài của clip ({data.audioClip.length}s).");
            return;
        }

        switch (data.audioType)
        {
            case AudioType.BGM:
                StopCoroutineIfRunning(ref bgmDurationCoroutine);

                bgmSource.clip = data.audioClip;
                bgmSource.volume = data.volume;
                bgmSource.pitch = data.pitch;
                bgmSource.loop = data.loop;

                bgmSource.time = startTime; // Đặt thời gian bắt đầu
                bgmSource.Play();

                if (duration > 0f)
                {
                    bgmDurationCoroutine = StartCoroutine(StopAudioAfterDelay(bgmSource, duration));
                }
                break;

            case AudioType.SFX:
                // LƯU Ý: Hàm PlayOneShot() mặc định của Unity KHÔNG hỗ trợ tùy chỉnh tua thời gian (startTime).
                // Do đó, nếu bạn muốn phát một PHẦN của SFX, hệ thống buộc phải chuyển sang dùng cơ chế Play() truyền thống.
                if (startTime == 0f && duration < 0f)
                {
                    // Phát SFX bình thường (Full bài, có thể phát chồng lên nhau)
                    sfxSource.pitch = data.pitch;
                    sfxSource.PlayOneShot(data.audioClip, data.volume);
                }
                else
                {
                    // Phát một phần SFX (Lúc này SFX này sẽ ngắt các SFX một phần khác đang chạy trước đó)
                    StopCoroutineIfRunning(ref sfxDurationCoroutine);

                    sfxSource.clip = data.audioClip;
                    sfxSource.volume = data.volume;
                    sfxSource.pitch = data.pitch;
                    sfxSource.loop = data.loop;

                    sfxSource.time = startTime;
                    sfxSource.Play();

                    // Nếu không truyền duration cụ thể, tự tính thời gian còn lại của clip
                    float actualDuration = duration > 0f ? duration : (data.audioClip.length - startTime);
                    sfxDurationCoroutine = StartCoroutine(StopAudioAfterDelay(sfxSource, actualDuration));
                }
                break;

            case AudioType.Dialogue:
                StopCoroutineIfRunning(ref dialogueDurationCoroutine);

                dialogueSource.clip = data.audioClip;
                dialogueSource.volume = data.volume;
                dialogueSource.pitch = data.pitch;
                dialogueSource.loop = data.loop;

                dialogueSource.time = startTime;
                dialogueSource.Play();

                if (duration > 0f)
                {
                    dialogueDurationCoroutine = StartCoroutine(StopAudioAfterDelay(dialogueSource, duration));
                }
                break;
        }
    }

    // Coroutine đợi hết thời gian yêu cầu rồi tắt AudioSource
    private IEnumerator StopAudioAfterDelay(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (source != null && source.isPlaying)
        {
            source.Stop();
        }
    }

    private void StopCoroutineIfRunning(ref Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    public void StopBGM() { StopCoroutineIfRunning(ref bgmDurationCoroutine); bgmSource.Stop(); }
    public void StopAllSFX() { StopCoroutineIfRunning(ref sfxDurationCoroutine); sfxSource.Stop(); }
    public void StopDialogue() { StopCoroutineIfRunning(ref dialogueDurationCoroutine); dialogueSource.Stop(); }
}