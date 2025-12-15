using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 카메라가 따라다닐 오브젝트
    public float smoothSpeed = 0.05f;  
    public Vector3 offset;  // 카메라와 오브젝트 사이의 거리

    void Start()
    {
        // offset 값을 카메라 z값과 동일하게 설정
        offset = new Vector3(0, 1, transform.position.z);
    }
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        // Vector3.Lerp(현재 위치, 가고 싶은 위치, 0~1사이 속도) = 현재 위치 + (가고 싶은 위치 - 현재 위치) * 속도
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
