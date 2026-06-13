using UnityEngine;
using UnityEngine.AI;

public class ClickToMove2DPlus : MonoBehaviour
{
    private NavMeshAgent agent;

    [Header("Cấu hình quét điểm")]
    [SerializeField] private float maxSampleDistance = 1.0f; // Bán kính tối đa để tìm điểm hợp lệ quanh cú click

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
            targetPos.z = transform.position.z;

            // Kiểm tra và nắn dòng vị trí click chuột bằng SamplePosition
            NavMeshHit hit;

            // Hàm này sẽ quét xung quanh điểm targetPos trong phạm vi maxSampleDistance
            // để tìm ra điểm nằm TRÊN LƯỚI XANH (Walkable) gần nhất.
            if (NavMesh.SamplePosition(targetPos, out hit, maxSampleDistance, NavMesh.AllAreas))
            {
                // Nếu tìm thấy điểm hợp lệ, bắt nhân vật đi tới điểm ĐÃ NẮN DÒNG (hit.position)
                // chứ không đi tới điểm click gốc (targetPos) nữa.
                agent.SetDestination(hit.position);
            }
            else
            {
                // Tùy chọn: Nếu người chơi click quá sâu vào vùng cấm (vượt quá maxSampleDistance)
                // Bạn có thể chọn không cho nhân vật di chuyển để tránh lỗi rướn.
                Debug.Log("Click vào vùng quá xa lưới di chuyển!");
            }
        }
    }
}