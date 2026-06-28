using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SpriteRenderer))]
public class ClickToMove2DPlus : MonoBehaviour
{
    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponent<SpriteRenderer>();

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

            // --- ĐIỀU CHỈNH VỊ TRÍ BẮN TIA RAYCAST XUỐNG DƯỚI ---
            Vector2 origin = transform.position;
            Vector2 offset = new Vector2(0, -0.5f); // Điều chỉnh xuống dưới 0.5 unit
            origin += offset;

            // Bắn một tia Ray dạng điểm ngay tại vị trí chuột
            RaycastHit2D hitObstacle = Physics2D.Raycast(origin, Vector2.zero, 0f, unwalkableLayer);

            if (hitObstacle.collider != null)
            {
                Debug.Log($"Click trúng vật thể thuộc vùng cấm: {hitObstacle.collider.name}. Hủy di chuyển!");
                return; // Thoát hàm luôn, không cho nhân vật chạy hoặc tính toán NavMesh nữa
            }

            // --- NẾU KHÔNG TRÚNG VẬT CẢN, TIẾP TỤC XỬ LÝ NAVMESH NHƯ CŨ ---
            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetPos, out hit, maxSampleDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);

                // Chỉ hoàn thành tutorial khi nhân vật thực sự di chuyển hợp lệ
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