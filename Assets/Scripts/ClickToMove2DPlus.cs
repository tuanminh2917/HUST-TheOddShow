using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SpriteRenderer))]
public class ClickToMove2DPlus : MonoBehaviour
{
    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;

    [Header("Cấu hình quét điểm")]
    [SerializeField] private float maxSampleDistance = 1.0f; // Bán kính tối đa để tìm điểm hợp lệ quanh cú click

    [Header("Sprites Hướng Di Chuyển (4 Hướng)")]
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private Sprite spriteDown;
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Tắt tính năng tự xoay của Agent trong không gian 3D để phù hợp game 2D
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        // 1. Xử lý nhận lệnh di chuyển bằng click chuột
        HandleClickToMove();

        // 2. Cập nhật Sprite theo hướng di chuyển thực tế của nhân vật
        UpdateSpriteDirection();
    }

    void HandleClickToMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
            targetPos.z = transform.position.z;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetPos, out hit, maxSampleDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            else
            {
                Debug.Log("Click vào vùng quá xa lưới di chuyển!");
            }
        }
    }

    void UpdateSpriteDirection()
    {
        // Lấy vận tốc hiện tại của Agent
        Vector3 velocity = agent.velocity;

        // Kiểm tra xem nhân vật có đang thực sự di chuyển hay không (tránh sai số nhỏ)
        if (velocity.sqrMagnitude > 0.005f)
        {
            // So sánh độ lớn tuyệt đối giữa trục X và trục Y để biết đang đi thiên về hướng nào hơn
            if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
            {
                // Đi ngang là chủ đạo
                if (velocity.x > 0)
                {
                    ChangeSprite(spriteRight); // Sang phải
                }
                else
                {
                    ChangeSprite(spriteLeft);  // Sang trái
                }
            }
            else
            {
                // Đi dọc là chủ đạo
                if (velocity.y > 0)
                {
                    ChangeSprite(spriteUp);    // Lên trên
                }
                else
                {
                    ChangeSprite(spriteDown);  // Xuống dưới
                }
            }
        }
        // Nếu agent dừng lại (velocity bằng 0), nhân vật sẽ giữ nguyên Sprite hướng cuối cùng mà nó đi
    }

    void ChangeSprite(Sprite newSprite)
    {
        // Chỉ thay đổi nếu sprite mới khác với sprite hiện tại để tối ưu hiệu năng
        if (spriteRenderer.sprite != newSprite && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }
}