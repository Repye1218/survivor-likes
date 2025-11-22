using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;

    private Vector3 offset;

    void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;
        
        // 목표 위치 계산 (플레이어 위치 + 초기 거리)
        Vector3 targetPosition = target.position + offset;
        
        // 현재 위치에서 목표 위치까지 smoothSpeed 속도로 섞어서 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
    }
}
