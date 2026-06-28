using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SpriteRenderer))]
public class ClickToMove2DPlus1 : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Cấu hình quét điểm")]
    [SerializeField] private float maxSampleDistance = 1.0f;

    // --- THÊM BIẾN NÀY ĐỂ QUẢN LÝ LAYER KHÔNG CHO PHÉP ĐI ---
    [Header("Cấu hình Chặn Di Chuyển")]
    [SerializeField] private LayerMask unwalkableLayer;

    [Header("Sprites Hướng Di Chuyển (4 Hướng)")]
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private Sprite spriteDown;
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        HandleClickToMove();
        UpdateSpriteDirection();
    }

    void HandleClickToMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
            targetPos.z = transform.position.z;

            // --- SỬA TẠI ĐÂY: Bắn tia tại điểm click chuột (targetPos) chứ không dùng origin của nhân vật ---
            RaycastHit2D hitObstacle = Physics2D.Raycast(targetPos, Vector2.zero, 0f, unwalkableLayer);

            if (hitObstacle.collider != null)
            {
                Debug.Log($"Click TRỰC TIẾP trúng vật thể vùng cấm: {hitObstacle.collider.name}. Hủy di chuyển!");
                return;
            }

            // --- NẾU KHÔNG TRÚNG VẬT CẢN TRỰC TIẾP, KIỂM TRA SAI SỐ NAVMESH ---
            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetPos, out hit, maxSampleDistance, NavMesh.AllAreas))
            {
                // Đo khoảng cách giữa điểm click chuột và điểm rìa NavMesh tìm được
                float distanceToMesh = Vector2.Distance(targetPos, hit.position);

                // Nếu click quá sát hoặc lọt hẳn vào trong mép vật cản lớn (sai số lớn hơn 0.35 đơn vị)
                if (distanceToMesh > 0.35f)
                {
                    Debug.Log($"Vị trí click quá sát mép hoặc nằm trong vùng cấm ({distanceToMesh}m). Hủy di chuyển tránh giật!");
                    return;
                }

                // Nếu mọi thứ hợp lệ, cho Agent di chuyển
                agent.SetDestination(hit.position);

                if (TutorialManager.Instance != null)
                {
                    TutorialManager.Instance.CompleteTutorial(TutorialType.ClickToMove);
                }
            }
            else
            {
                Debug.Log("Click vào vùng quá xa lưới di chuyển!");
            }
        }
    }
    void UpdateSpriteDirection()
    {
        Vector3 velocity = agent.velocity;

        if (velocity.sqrMagnitude > 0.005f)
        {
            if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
            {
                if (velocity.x > 0) ChangeSprite(spriteRight);
                else ChangeSprite(spriteLeft);
            }
            else
            {
                if (velocity.y > 0) ChangeSprite(spriteUp);
                else ChangeSprite(spriteDown);
            }
        }
    }

    void ChangeSprite(Sprite newSprite)
    {
        if (spriteRenderer.sprite != newSprite && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }
}