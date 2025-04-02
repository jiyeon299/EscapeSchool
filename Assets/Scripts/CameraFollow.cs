using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 트랜스폼
    public float smoothSpeed = 0.125f; // 부드러운 이동 속도
    public Vector3 offset; // 카메라와 플레이어의 거리 조정 (X, Y, Z)

    void LateUpdate()
    {
        // 카메라가 따라갈 목표 위치 (플레이어 위치 + offset)
        Vector3 desiredPosition = player.position + offset;

        // 카메라가 부드럽게 이동하도록
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, 
            desiredPosition, smoothSpeed);

        // Y값은 고정하고, X값만 따라가도록 설정
        transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z); // Y는 고정, X만 따라감
    }

}
